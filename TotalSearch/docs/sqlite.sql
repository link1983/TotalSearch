create table Files(
id integer PRIMARY KEY autoincrement,
md5 varchar(100),
fullname txt,
modifiedtime varchar(100),
gettime varchar(100),
parsetime varchar(100),
content txt
)

--存储关注目录下的所有文件，用来检测对比
create table LatestFiles(
id integer PRIMARY KEY autoincrement,
md5 varchar(100),
fullname txt
)

--存储关注的目录
create table Directories(
id integer PRIMARY KEY autoincrement,
dir txt
)

select count(*) from files
select * from files where fullname like '%Linux%'