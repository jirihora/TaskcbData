namespace OrderAggregator.Settings
{
    /// <summary>
    /// Settings for <see cref="OrderAggregatorPeriodicService"/>.
    /// </summary>
    public class OrderAggregatorPeriodicServiceSettings
    {
        /// <summary>
        /// Order aggregation interval in seconds.
        /// </summary>
        public int OrderAggregationInterval { get; set; }

        /// <summary>
        /// Checks if settings is set to default.
        /// </summary>
        /// <returns>True if all values are default.</returns>
        public bool IsDefault()
        {
            return OrderAggregationInterval == default;
        }
    }
}
