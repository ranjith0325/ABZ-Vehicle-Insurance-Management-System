CREATE TABLE [dbo].[CustomerQuery]
(
	QueryID CHAR(10) primary key,
	CustomerID CHAR(10) NOT NULL,
	foreign key(CustomerID) references Customer(CustomerID),
	Description VARCHAR(100),
	QueryDate DATETIME,
	Status CHAR(1) check (status in ('A','R'))
)
