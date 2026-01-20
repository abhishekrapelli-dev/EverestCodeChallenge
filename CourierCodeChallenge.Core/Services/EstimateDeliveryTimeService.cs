using CourierCodeChallenge.Core.Models;

namespace CourierCodeChallenge.Core.Services
{
    public class EstimateDeliveryTimeService : EstimateDeliveryCostService
    {
        public int _numberOfVehicles { get; }
        public int _maxSpeed { get; }
        public int _maxWeight { get; }

        public EstimateDeliveryTimeService(int vehicleCount, int maxSpeed, 
            int maxLoad, int basePrice) : base(basePrice)
        {
            _numberOfVehicles = vehicleCount;
            _maxSpeed = maxSpeed;
            _maxWeight = maxLoad;
        }

        public override void Calculate(List<Package> packages)
        {
            // Calculate Delivery Cost 
            base.Calculate(packages);

            // Min-heap based on vehicle availability time
            var vehicleQueue = new PriorityQueue<Vehicle, double>();

            for (int i = 0; i < _numberOfVehicles; i++)
            {
                vehicleQueue.Enqueue(new Vehicle { AvailableAt = 0 }, 0);
            }

            while (packages.Any(p => !p.IsDelivered))
            {
                // Get earliest available vehicle
                var vehicle = vehicleQueue.Dequeue();
                double currentTime = vehicle.AvailableAt;

                var remainingPackages = packages
                    .Where(p => !p.IsDelivered)
                    .ToList();

                var shipment = SelectBestShipment(remainingPackages);

                double maxDistanceInTrip = 0;

                foreach (var pkg in shipment)
                {
                    double deliveryTime = currentTime + (double)pkg.Distance / _maxSpeed;

                    pkg.DeliveryTime = Math.Truncate(deliveryTime * 100) / 100;
                    pkg.IsDelivered = true;

                    maxDistanceInTrip = Math.Max(maxDistanceInTrip, pkg.Distance);
                }

                double roundTripTime = 2 * (Math.Truncate(maxDistanceInTrip / _maxSpeed * 100) / 100);
                vehicle.AvailableAt = currentTime + roundTripTime;
                vehicleQueue.Enqueue(vehicle, vehicle.AvailableAt);
            }
        }

        private List<Package> SelectBestShipment(List<Package> packages)
        {
            List<Package> bestShipment = new();

            var allCombinations = GetValidCombinations(packages);

            foreach (var shipment in allCombinations)
            {
                if (bestShipment.Count == 0)
                {
                    bestShipment = shipment;
                    continue;
                }

                if (shipment.Count > bestShipment.Count
                    || shipment.Count == bestShipment.Count &&
                        shipment.Sum(p => p.Weight) > bestShipment.Sum(p => p.Weight)
                    || shipment.Count == bestShipment.Count &&
                        shipment.Sum(p => p.Weight) == bestShipment.Sum(p => p.Weight) &&
                        shipment.Max(p => p.Distance) < bestShipment.Max(p => p.Distance))
                {
                    bestShipment = shipment;
                }
            }

            return bestShipment;
        }

        private List<List<Package>> GetValidCombinations(List<Package> packages)
        {
            var result = new List<List<Package>>();

            void Backtrack(int index, List<Package> current, int currentWeight)
            {
                if (current.Count > 0)
                    result.Add(new List<Package>(current));

                for (int i = index; i < packages.Count; i++)
                {
                    if (currentWeight + packages[i].Weight > _maxWeight)
                        continue;

                    current.Add(packages[i]);
                    Backtrack(i + 1, current, currentWeight + packages[i].Weight);
                    current.RemoveAt(current.Count - 1);
                }
            }

            Backtrack(0, new List<Package>(), 0);
            return result;
        }
    }
}
