namespace Logging
{
    /// <summary>
    /// Description of LogLevelEnum.
    /// </summary>
    public enum LogLevelEnum
    {
        /// <summary>
        /// Lowest level. All messages get logged.
        /// </summary>
		Debug,

        /// <summary>
        /// Good for testing, information for developers not end users
        /// </summary>
		Verbose,

        /// <summary>
        /// Standard output, normal customer facing level
        /// </summary>
		Info,

        /// <summary>
        /// Not an error, but might point to one
        /// </summary>
		Warning,

        /// <summary>
        /// All hell has broken lose, and the ship is going down.
        /// </summary>
		Error
    }
}