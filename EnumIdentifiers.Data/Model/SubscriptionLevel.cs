namespace EnumIdentifiers.Data.Model
{
    public class SubscriptionLevel
    {
        public enum Level
        {
            Basic,
            Standard,
            Premium
        }

        public enum VideoQuality
        {
            Standard,
            HD,
            UHD
        }

        public Level Name { get; set; }
        public decimal PricePerMonth { get; set; }
        public int NumberOfSimultaneousDevices { get; set; }
        public int NumberDevicesWithDownloadCapability { get; set; }
        public VideoQuality Quality { get; set; }
    }
}