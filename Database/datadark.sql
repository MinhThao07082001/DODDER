go
--drop database PRN211
create database PRN211
go
use PRN211
go
create table gender(
id int identity(1,1) primary key,
name nvarchar(50),
)
create table relation(
id int identity(1,1) primary key,
name nvarchar(50),
)
create table user_account(
id int identity(1,1) primary key,
email nvarchar(30) not null unique,
[password] nvarchar(30) not null,
phone nvarchar(12) not null unique,
name nvarchar(50) not null,
nickname nvarchar(500),
gender_id int not null,
dob date not null,
latitude float not null,
longitude float not null,
[address] nvarchar(200),
foreign key (gender_id) references gender(id),
introduce nvarchar(500),
moneyLeft int default 0,
[status] nvarchar(1000),
ageFrom int default 18,
ageTo int default 30,
[authentication] int default 0,
create_time datetime default getDate()
)
create table user_photo(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
link nvarchar(1000),
detail nvarchar(1000),
photo_type int,
create_time datetime default getDate(),
)
create table gender_interested(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
gender_id int,
foreign key (gender_id) references gender(id),
create_time datetime default getDate()
)
create table relation_interested(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
relation_id int,
foreign key (relation_id) references relation(id),
create_time datetime default getDate()
)
create table user_like(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
user_account_id_like int,
foreign key (user_account_id_like) references user_account(id),
create_time datetime default getDate()
)
create table user_block(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
user_account_id_block int,
foreign key (user_account_id_block) references user_account(id),
create_time datetime default getDate()
)
create table user_dislike(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
user_account_id_dislike int,
foreign key (user_account_id_dislike) references user_account(id),
create_time datetime default getDate()
)
create table user_report(
id int identity(1,1) primary key,
user_account_id int,
foreign key (user_account_id) references user_account(id),
user_account_id_report int,
foreign key (user_account_id_report) references user_account(id),
details nvarchar(500),
create_time datetime default getDate()
)
create table [conversation](
id int identity(1,1) primary key,
user_account_id_creator int,
foreign key (user_account_id_creator) references user_account(id),
user_account_id_2 int,
foreign key (user_account_id_2) references user_account(id),
create_time datetime default getDate()
)
create table [message](
id int identity(1,1) primary key,
conversation_id int,
user_account_id_sender int,
foreign key (user_account_id_sender) references user_account(id),
user_account_id_receiver int,
foreign key (user_account_id_receiver) references user_account(id),
content nvarchar(1000),
foreign key (conversation_id) references conversation(id),
status int,
createTime datetime default getDate(),
)
create table [admin](
id int identity(1,1) primary key,
email nvarchar(50),
password nvarchar(50),
)
CREATE TABLE [notification](
id int identity(1,1) primary key,
user_account_id int, 
foreign key (user_account_id) references user_account(id),
content nvarchar(1000),
/*ex: 1 is message, 2 is new request, 3 is money...*/
notiType int,
status int,
createTime datetime default getDate(),
)
create table hobby(
id int identity(1,1) primary key,
name nvarchar(50),
)
create table user_hobby(
hobby_id int,
foreign key (hobby_id) references hobby(id),
user_account_id int, 
foreign key (user_account_id) references user_account(id),
createTime datetime default getDate(),
)
create table upgrade_service(
id int identity(1,1) primary key,
name nvarchar(50),
details nvarchar(500),
price int,
)
create table bill(
upgrade_service_id int,
foreign key (upgrade_service_id) references upgrade_service(id),
user_account_id int, 
foreign key (user_account_id) references user_account(id),
service_month int,
price int,
createTime datetime default getDate(),
)
CREATE TABLE authentication_code(
id int identity(1,1) primary key,
email nvarchar(100),
code nvarchar(10),
createTime datetime default getDate(),
)