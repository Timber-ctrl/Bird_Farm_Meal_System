Use master
Go
Create Database BirdFarmMealSystem
Go
Use BirdFarmMealSystem
Go
Create Table [Admin] (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	AvatarUrl nvarchar(max),
	Email nvarchar(256) unique not null,
	Password nvarchar(256) not null,
	DeviceToken nvarchar(max),
	CreateAt datetime not null default getdate()
)
Go
Create Table Manager (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	AvatarUrl nvarchar(max),
	Email nvarchar(256) unique not null,
	Phone nvarchar(256),
	Password nvarchar(256) not null,
	Status nvarchar(256) not null,
	DeviceToken nvarchar(max),
	CreateAt datetime not null default getdate()
)
Go
Create Table Farm (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	ThumbnailUrl nvarchar(max),
	Address nvarchar(max) not null,
	Phone nvarchar(256) not null,
	ManagerId uniqueidentifier foreign key references Manager(Id) unique not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table Area (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	ThumbnailUrl nvarchar(max),
	FarmId uniqueidentifier foreign key references Farm(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table Staff (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	AvatarUrl nvarchar(max),
	Email nvarchar(256) unique not null,
	Phone nvarchar(256) unique,
	Password nvarchar(256) not null,
	Status nvarchar(256) not null,
	DeviceToken nvarchar(max),
	FarmId uniqueidentifier foreign key references Farm(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table CareMode (
	Id uniqueidentifier primary key,
	Priority int not null,
	Name nvarchar(256) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table BirdCategory (
	Id uniqueidentifier primary key,
	ThumbnailUrl nvarchar(max),
	Name nvarchar(256) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table Species (
	Id uniqueidentifier primary key,
	ThumbnailUrl nvarchar(max),
	Name nvarchar(256) not null,
	BirdCategoryId uniqueidentifier foreign key references BirdCategory(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table Cage (
	Id uniqueidentifier primary key,
	Code nvarchar(256) not null,
	Name nvarchar(256) not null,
	Material nvarchar(256) not null,
	Description nvarchar(max),
	Height float not null,
	Width float not null,
	Depth float not null,
	ThumbnailUrl nvarchar(max),
	AreaId uniqueidentifier foreign key references Area(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table CageSpecies (
	CageId uniqueidentifier foreign key references Cage(Id) not null,
	SpeciesId uniqueidentifier foreign key references Species(Id) not null,
	CreateAt datetime not null default getdate(),
	Primary key (CageId, SpeciesId)
)
Go
Create Table Bird (
	Id uniqueidentifier primary key,
	ThumbnailUrl nvarchar(max),
	Characteristic nvarchar(max),
	Name nvarchar(256) not null,
	Gender bit not null,
	DayOfBirth datetime not null,
	Code nvarchar(256),
	CageId uniqueidentifier foreign key references Cage(Id) not null,
	SpeciesId uniqueidentifier foreign key references Species(Id) not null,
	CategoryId uniqueidentifier foreign key references BirdCategory(Id) not null,
	CareModeId uniqueidentifier foreign key references CareMode(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table FoodCategory (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table UnitOfMeasurement (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table Food (
	Id uniqueidentifier primary key,
	ThumbnailUrl nvarchar(max) not null,
	Name nvarchar(256) not null,
	FoodCategoryId uniqueidentifier foreign key references FoodCategory(Id) not null,
	Quantity float not null,
	UnitOfMeasurementId uniqueidentifier foreign key references UnitOfMeasurement(Id) not null,
	Status nvarchar(256) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table MenuSammple (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	SpeciesId uniqueidentifier foreign key references Species(Id) not null,
	CareModeId uniqueidentifier foreign key references CareMode(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table MenuMealSample (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	[From] time not null,
	[To] time not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table MealItemSample (
	MenuMealSammpleId uniqueidentifier foreign key references MenuMealSample(Id) not null,
	FoodId  uniqueidentifier foreign key references Food(Id) not null,
	Quantity float not null,
	[Order] int not null,
	Primary key (MenuMealSammpleId, FoodId)
)
Go
Create Table Menu (
	Id uniqueidentifier primary key,
	Name nvarchar(256) not null,
	SpeciesId uniqueidentifier foreign key references Species(Id) not null,
	CareModeId uniqueidentifier foreign key references CareMode(Id) not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table MenuMeal (
	Id uniqueidentifier primary key,
	MenuId uniqueidentifier foreign key references Menu(Id) not null, 
	Name nvarchar(256) not null,
	[From] time not null,
	[To] time not null,
	CreateAt datetime not null default getdate()
)
Go
Create Table MealItem (
	MenuMealId uniqueidentifier foreign key references MenuMeal(Id) not null,
	FoodId  uniqueidentifier foreign key references Food(Id) not null,
	Quantity float not null,
	[Order] int not null,
	Primary key (MenuMealId, FoodId)
)
Go
Create Table [Plan] (
	Id uniqueidentifier primary key,
	Title nvarchar(256) not null,
	[From] datetime not null,
	[To] datetime not null,
	MenuId uniqueidentifier foreign key references Menu(Id) not null, 
	CageId uniqueidentifier foreign key references Cage(Id) not null,
	CreateAt datetime not null default getdate(),
)
Go
Create Table Task (
	Id uniqueidentifier primary key,
	CageId uniqueidentifier foreign key references Cage(Id) not null, 
	Title nvarchar(max) not null,
	Description nvarchar(max) not null,
	ManagerId uniqueidentifier foreign key references Manager(Id) not null, 
	Deadline datetime not null,
	CreateAt datetime not null default getdate(),
)
Go
Create Table AssignStaff (
	TaskId uniqueidentifier foreign key references Task(Id) not null,
	StaffId uniqueidentifier foreign key references Staff(Id) not null,
	CreateAt datetime not null default getdate(),
	Primary key (TaskId, StaffId)
)
Go
Create Table TaskCheckList (
	Id uniqueidentifier primary key,
	Title nvarchar(max) not null,
	TaskId uniqueidentifier foreign key references Task(Id) not null, 
	AsigneeId uniqueidentifier foreign key references Staff(Id) not null,
	Status bit not null default 0,
	[Order] int not null,
	CreateAt datetime not null default getdate(),
)
Go
Create Table TaskCheckListReport (
	Id uniqueidentifier primary key,
	TaskCheckListId uniqueidentifier foreign key references TaskCheckList(Id) not null,
	FinishAt datetime not null,
)
Go
Create Table TaskCheckListReportItem (
	Id uniqueidentifier primary key,
	TaskCheckListReportId uniqueidentifier foreign key references TaskCheckListReport(Id) not null,
	Issue nvarchar(256) not null,
	Positive bit not null,
	Severity int not null,
	Message nvarchar(max)
)
Go
Create Table TaskSample (
	Id uniqueidentifier primary key,
	ThumbnailUrl nvarchar(max) not null,
	Name nvarchar(256) not null,
	Description nvarchar(max) not null,
	CareModeId uniqueidentifier foreign key references CareMode(Id) not null,
	CreateAt datetime not null default getdate(),
)
Go
Create Table TaskSampleCheckList (
	Id uniqueidentifier primary key,
	Title nvarchar(max) not null,
	TaskSampleId uniqueidentifier foreign key references TaskSample(Id) not null,
	[Order] int not null,
	CreateAt datetime not null default getdate(),
)
Go
Create Table [Repeat] (
	Id uniqueidentifier primary key,
	Type nvarchar(256) not null,
	Time int not null,
	Until datetime not null,
	TaskId uniqueidentifier foreign key references Task(Id),
	TaskSampleId uniqueidentifier foreign key references TaskSample(Id),
	CONSTRAINT CHK_EitherTaskOrTaskSample
        CHECK (
            (TaskId IS NOT NULL AND TaskSampleId IS NULL)
            OR
            (TaskId IS NULL AND TaskSampleId IS NOT NULL)
        ),
)
Go
Create Table Ticket (
	Id uniqueidentifier primary key,
	TicketCategory nvarchar(256) not null,
	CreatorId uniqueidentifier foreign key references Staff(Id) not null,
	Priority nvarchar(256) not null,
	AssigneeId uniqueidentifier foreign key references Staff(Id),
	CageId uniqueidentifier foreign key references Cage(Id),
	Description nvarchar(max) not null,
	Image nvarchar(max) not null,
	Status nvarchar(256) not null,
	CreateAt datetime not null default getdate(),
)