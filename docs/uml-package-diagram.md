# UML Package Diagram

```plantuml
@startuml
package "Fred" {
  package "src/Application" {
    class Queries
    class QueryResults
    class ServiceExtensions
  }

  package "src/Infrastructure" {
    package "Wilma" {
      package "Interfaces"
      package "Models"
      class ServiceExtensions
      class WilmaOptions
    }
    class QueryHandlers
    class ServiceExtensions
  }

  package "src/WebUI" {
    class Program
    package "Components"
  }
}

package "Wilma" {
  package "src/WebApi" {
    class Program
    class WeatherForecast
    package "Controllers"
  }
}

"Fred/src/Application" --> "Fred/src/Infrastructure" : query/handler dependencies
"Fred/src/Infrastructure" --> "Wilma/src/WebApi" : HTTP client integration
"Fred/src/WebUI" --> "Fred/src/Application" : command/query usage
@enduml
```
