select * from sys.all_objects

SELECT name, type, is_disabled FROM sys.server_principals  

{"Cannot insert the value NULL into column 'user_code', table 'SpringNetExampleTest.dbo.FM_USER'; column does not allow nulls. INSERT fails.\r\nThe statement has been terminated."}

ALTER LOGIN sa ENABLE  
GO  
ALTER LOGIN sa WITH PASSWORD='Star0203'  
GO  
SELECT 
    [Extent1].[ID] AS [ID], 
    [Extent1].[ROLE_NAME] AS [ROLE_NAME], 
    [Extent1].[REMARK] AS [REMARK], 
    [Extent1].[STATE] AS [STATE], 
    [Extent1].[SYS_APP_KEY] AS [SYS_APP_KEY]
    FROM [dbo].[FM_ROLE] AS [Extent1]
select * from  sysobjects where xtype='U'

create database SpringNetExampleTest

select * from syscolumns where id= OBJECT_ID('FM_USER')

select * from FM_USER
drop table FM_USER
-- Create table
create table FM_USER
(
  ID                     int  ,
  user_code              varchar(20) not null,
  [user_name]              varchar(100) not null,
  user_password          varchar(200) not null,
  user_telephone         varchar(30),
  user_email             varchar(64),
  user_type              CHAR(1) not null,
  sort                   decimal(10) default 0 not null,
  remark                 varchar(256),
  enable_flag            CHAR(1) not null,
  issys_flag             CHAR(1) not null,
  error_times            INTEGER,
  last_error_time        DATE,
  latest_update_time     DATE,
  org_id                 varchar(32),
  startdate              DATE,
  expirydate             DATE,
  latest_password_update DATE ,
  login_key              varchar(15)
)