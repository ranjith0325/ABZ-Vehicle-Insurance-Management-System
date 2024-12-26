CREATE TABLE [dbo].[QueryResponse]
(
	QueryID CHAR(10) references CustomerQuery(QueryID),
	SrNo CHAR(10),
	AgentID CHAR(10) references Agent(AgentID),
	Description VARCHAR(100),
	ResponseDate DATETIME
	PRIMARY KEY(QueryID,SrNo)
)
