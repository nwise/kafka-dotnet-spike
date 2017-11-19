# Kafka Spike

## Getting Started
* Start Kafka
  * `docker-compose up --build`

* Start Producer
  * `cd <project-root>/kafka.producer`
  * `dotnet restore`
  * `dotnet run`

* Start Consumer A:1
  * `cd <project-root>/kafka.consumer`
  * `dotnet restore`
  * `dotnet run A 1`

* Start Consumer A:2
  * `cd <project-root>/kafka.consumer`
  * `dotnet restore`
  * `dotnet run A 2`

* Start Consumer B:1
  * `cd <project-root>/kafka.consumer`
  * `dotnet restore`
  * `dotnet run B 1`

