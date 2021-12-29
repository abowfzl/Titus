# Name
 Titus

# Usage
Read Huge Data From Ecxel (like 500,000 Rows that use in Project) And Insert To Database.

## Summery
Read Excel File And Check Dublicated Value And Insert Values to Redis and In [Program.cs](https://github.com/abowfzl/Titus/blob/master/KaveNegarTest/Program.cs/)
<br>
Read again from Redis and Insert to SQL With Fastest Performance.

## Steps

1.Open Excel Stream and Read Data.</br>
2.Validate Data and Insert Data In Redis.</br>
3.Read Data From Redis.</br>
4.Insert Data In SQL.</br>

# About
An Cosole Application with 3 ClassLibrary (3 Layer)</br>
Core,Data,Servie </br>
Repository Pattern For EF DbContext and Redis Wrapper in Core Layer</br>
Models In Data</br>
Services In Service Layer</br>


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
