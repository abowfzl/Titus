# Name
 Titus

# Usage
Read Huge Data From Ecxel (like 500,000 Rows that use in Project) And Insert To Database.

## Summery
Read Excel File And Check Dublicated Value And Insert Values to Redis In [Program.cs](https://github.com/abowfzl/Titus/blob/master/KaveNegarTest/Program.cs/)
,<br>
Read again from Redis and Insert to SQL With Fastest Performance.

## Steps

1. Open Excel Stream and Read Data.</br>
2. Validate Data and Insert Data In Redis.</br>
3. Read Data From Redis.</br>
4. Insert Data In SQL.</br>

# About
An Cosole Application built with .Net 5 with 2 ClassLibrary (3 Layer (Core, Domain, Service)).</br></br>
Repository Pattern For EF DbContext and Redis Wrapper in [Core](https://github.com/abowfzl/Titus/blob/master/Core) Layer,</br>
Models In [Domain](https://github.com/abowfzl/Titus/blob/master/Core/Domain/),</br>
Services and Distributed CacheManager Methods Implemention In [Service](https://github.com/abowfzl/Titus/blob/master/Service) Layer,</br>
Unit Tests with Moq Repository and Fake Services for Test Domain Services In [UnitTest](https://github.com/abowfzl/Titus/blob/master/UnitTest)


### Built With

* [.Net](https://dotnet.microsoft.com/en-us/)
* [Entityframework Core](https://docs.microsoft.com/en-us/ef/core/)

### Redis
* [StackExchangeRedis](https://github.com/StackExchange/StackExchange.Redis)

### Working with Excel:
* [EPPluse](https://www.epplussoftware.com/)

### Unit Test:
* [Moq]()
* [NUnit]()
