DELETE FROM IssueTracker.dbo.[User]
INSERT INTO IssueTracker.dbo.[User] ([Username], [Password], [Email], [Name])
SELECT * FROM IssueTracker_Old.dbo.[User]

DELETE FROM IssueTracker.dbo.[Status]
INSERT INTO IssueTracker.dbo.[Status] ([Name], [Reactivate])
SELECT * FROM IssueTracker_Old.dbo.[Status]

DELETE FROM IssueTracker.dbo.[DiscardRule]
INSERT INTO IssueTracker.dbo.[DiscardRule] ([Id], [Text], [Creator], [ServerVariables], [StackTrace])
SELECT * FROM IssueTracker_Old.dbo.[DiscardRules]

SET IDENTITY_INSERT IssueTracker.dbo.[Issue] ON
DELETE FROM IssueTracker.dbo.[Issue]
INSERT INTO IssueTracker.dbo.[Issue] ([Id], [Creator], [DateOfCreation], [Status], [Text], [StackTrace], [ServerVariables], [ParentIssueId], [IsPublic], [AssignedTo])
SELECT * FROM IssueTracker_Old.dbo.[Issue]
SET IDENTITY_INSERT IssueTracker.dbo.[Issue] OFF

SET IDENTITY_INSERT IssueTracker.dbo.[Comment] ON
DELETE FROM IssueTracker.dbo.[Comment]
INSERT INTO IssueTracker.dbo.[Comment] ([Id], [IssueId], [Creator], [DateOfCreation], [Text], [AttachmentFileName], [AttachmentNiceName], [DuplicateIssueId], [Email], [OldStatus], [NewStatus], [OldAssignedTo], [NewAssignedTo])
SELECT * FROM IssueTracker_Old.dbo.[Comment]
SET IDENTITY_INSERT IssueTracker.dbo.[Comment] OFF