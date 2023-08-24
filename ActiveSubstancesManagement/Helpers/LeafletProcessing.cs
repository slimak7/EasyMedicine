using ActiveSubstancesManagement.Dtos;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;

namespace ActiveSubstancesManagement.Helpers
{
    public static class LeafletProcessing
    {
        public static List<(Guid substanceID, int interactionLevel)> GetInteractedSubstances(LeafletAddedDto leaflet)
        {
            List<(Guid substanceID, int interactionLevelID)> interactions = new List<(Guid substanceID, int interactionLevelID)>();
            try
            {
                StatsCollector statsCollector = new StatsCollector(leaflet.MedicineID[0].ToString());
                statsCollector.Start();

                var document = UglyToad.PdfPig.PdfDocument.Open(leaflet.Leaflet);

                var translation = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveSubstancesManagement.XMLTranslations.Translation_PL.xml");
                var keywords = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveSubstancesManagement.Keywords.keywords_PL.txt");

                string translationString;
                List<(string keyword, int level)> keywordsList = new List<(string, int)>();

                using (StreamReader reader = new StreamReader(translation))
                {
                    translationString = reader.ReadToEnd();
                }

                using (StreamReader reader = new StreamReader(keywords))
                {
                    var keywordsString = reader.ReadToEnd();

                    var pairs = keywordsString.Split("\n").ToList();

                    foreach (var pair in pairs)
                    {
                        var elements = pair.Split(",");


                        keywordsList.Add((elements[0], int.Parse(elements[1])));
                    }
                }

                var leafletPages = document.GetPages().Select(x => x.Text).ToList();

                string leafletText = leafletPages.Aggregate((x, y) => x + "\n" + y);

                var sentences = leafletText.Split(".");

                Dictionary<string, string> translationPairs = new Dictionary<string, string>();

                using (XmlReader reader = XmlReader.Create(new StringReader(translationString)))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "ActiveSubstance")
                        {
                            string name = reader.GetAttribute("Name");
                            string guid = reader.GetAttribute("SubstanceID");

                            translationPairs.Add(name, guid);
                        }
                    }
                }

                foreach (var keyword in keywordsList)
                {
                    var foundSentences = Array.FindAll(sentences, (x => x.Contains(keyword.keyword)));

                    if (foundSentences.Any())
                    {
                        statsCollector.NumberOfFoundKeywords = foundSentences.Length;

                        foreach (var sentence in foundSentences)
                        {
                            foreach (var pair in translationPairs)
                            {
                                Regex regex = new Regex(@"." + pair.Key.Substring(0, pair.Key.Length - 1).Replace("(", " ").Replace(")", " ") + ".", RegexOptions.IgnoreCase);

                                if (regex.IsMatch(sentence))
                                {
                                    if (!leaflet.SubstancesID.Contains(new Guid(pair.Value)) && !interactions.Select(x => x.substanceID).Contains(new Guid(pair.Value)))
                                    {
                                        interactions.Add((new Guid(pair.Value), keyword.level));

                                        statsCollector.NumberOfFoundSubstances++;
                                    }
                                }
                            }
                        }
                    }
                }

                statsCollector.End();

                return interactions;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return interactions;
            }
        }
    }
}
