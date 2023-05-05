using MedicinesManagement.Dtos;

namespace MedicinesManagement.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewLeaflet(MedicineUpdateInfoDto newLeaflet);
    }
}
