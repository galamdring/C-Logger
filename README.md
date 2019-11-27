<b>C# Logger</b>


This logger is designed to handle any sort of logging class, as well as multiple logging locations, allowing for abstraction between sending something to the log and actually knowing anything about the log.

I've added two samples, one in .Net Framework (4.7.2) and one in .Net Core (3.0). Both implement a MessageBoxLogger that is used to create the loggers in LoggerFactory. Then an instance of MultiLogger with a name is retrieved and stored as the Log object. All logging is then done by calling the level methods on the Log object.

This abstraction could be used to allow runtime configuration of loggers, or just to simplify code updates such as switching logging libraries.

Any subsequent classes that need to log would only need to get an instance of the MultiLogger, and not need to know anything about how the logging itself is done.
