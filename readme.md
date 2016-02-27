#Shutdown Service
The Shutdown Service contains a Windows Service that monitors specific settings in your environment
and performs a shutdown of the computer if the conditions are not met. A use case for this can be a
computer that should shutdown if no consuming devices are online.

##Supported conditions
Currently the following conditions are supported:
* Computers with a specific IP or DNS name is online (determined by a "ping" to that computer)
* Time (supports multiple time ranges and can be configured individually for each day)

##Configuration
The configuration is performed using a XML file. An [example](Configuration/Configuration.xml) can 
be found in the Configuration project. A [XML schema](Configuration/Configuration.xsd) is available
as well.

Checks are performed for each entry in the configuration file in the interval specified in the
configuration file. The default state for each check is that the check allows the computer to 
shutdown. If the conditions of a check are met (e.g. computer(s) is online or the current time
matches one of the time constraints), the check evaluates to false. The computer is only shutdown
if all checks pass (e.g. there is no check that objects to the shutdown).

#Dependencies
The solution uses the .NET Framework 4.5 and obviously targets Windows computers. 

##Development Dependencies
Each log message has a unique number. These numbers are generated using a 
[T4](https://msdn.microsoft.com/en-us/library/bb126445.aspx) template that converts the content
of a txt file to a C# class. To write the T4 template only once, the conversion is performed by
a central template and uses the [T4 Toolbox](https://github.com/olegsych/T4Toolbox) to invoke
the template on each txt file. It is therefore required that the
[T4 Toolbox extension](https://visualstudiogallery.msdn.microsoft.com/34b6d489-afbc-4d7b-82c3-dded2b726dbc) 
is installed. 