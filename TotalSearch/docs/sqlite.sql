create table Files(
id integer PRIMARY KEY autoincrement,
md5 varchar(100),
fullname txt,
modifiedtime varchar(100),
gettime varchar(100),
parsetime varchar(100),
content txt
)

--�洢��עĿ¼�µ������ļ����������Ա�
create table LatestFiles(
id integer PRIMARY KEY autoincrement,
md5 varchar(100),
fullname txt
)

--�洢��ע��Ŀ¼
create table Directories(
id integer PRIMARY KEY autoincrement,
dir txt
)

select count(*) from files
select * from files where fullname like '%Linux%'