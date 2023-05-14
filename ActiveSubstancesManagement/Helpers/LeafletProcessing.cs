﻿using ActiveSubstancesManagement.Dtos;
using System.Reflection;

namespace ActiveSubstancesManagement.Helpers
{
    public static class LeafletProcessing
    {
        public static List<(Guid substanceID, Guid interactionLevelID)> GetInteractedSubstances(LeafletAddedDto leaflet)
        {
            var document = UglyToad.PdfPig.PdfDocument.Open(leaflet.Leaflet);

            var translation = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveSubstancesManagement.XMLTranslations.Translation_PL.xml");

            string translationString;

            using (StreamReader reader = new StreamReader(translation))
            {
                translationString = reader.ReadToEnd();
            }

            var leafletPages = document.GetPages().Select(x => x.Text).ToList();

            string leafletText = leafletPages.Aggregate((x,y) => x + "\n" + y);


            throw new NotImplementedException();
        }
    }
}
