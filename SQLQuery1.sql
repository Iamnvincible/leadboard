SELECT ID,email FROM dbo.AspNetUsers
SELECT id,Name FROM dbo.AspNetRoles
INSERT INTO dbo.AspNetUserRoles
        ( UserId, RoleId )
VALUES  ( (SELECT ID FROM dbo.AspNetUsers WHERE Email='l@y.c'), (SELECT id FROM dbo.AspNetRoles WHERE Name='Administrator'));
INSERT INTO dbo.AspNetUserRoles
        ( UserId, RoleId )
VALUES  ( (SELECT ID FROM dbo.AspNetUsers WHERE Email='test@t.t'), (SELECT id FROM dbo.AspNetRoles WHERE Name='User'));