
use OnlineFoodOrderDB;

Create table ApprovedUsers( Id int primary key not null identity(1,1),
							UserName varchar(30),
							Email nvarchar(30),
							UserPassword nvarchar(50),
							UserRole varchar(15), 
							UserPhone varchar(20), 
							UserLocation nvarchar(30),
							TempPassword nvarchar(10));



create table PendingUser( Id int primary key not null identity(1,1),
							UserName varchar(30),
							Email nvarchar(30),
							UserPassword nvarchar(50),
							UserRole varchar(15), 
							UserPhone varchar(20), 
							UserLocation nvarchar(30),
							TempPassword nvarchar(10));



create table Hotel (Id int primary key not null identity(1,1), 
					HotelName varchar(30),
					ApprovedUsersId int foreign key(ApprovedUsersId) references ApprovedUsers(Id));

create table HotelBranch (Id int primary key not null identity(1,1), 
						  BranchName varchar(30),
						  BranchLocation varchar(30),
						  BranchPhoneNumber varchar(20),
						  HotelId int foreign key(HotelId) references Hotel(Id));

create table MenuDetails (Id int primary key not null identity(1,1),
						  MenuItems varchar(50),
						  MenuQuantity int,
						  Price money,
						  HotelBranchId int foreign key (HotelBranchId) references HotelBranch(Id));

create table Orders(Id int primary key not null identity(1,1),
					ApprovedUsersId int foreign key (ApprovedUsersId) references ApprovedUsers(Id),
					HotelBranchId int foreign key (HotelBranchId) references HotelBranch(Id),
					MenuDetailsId int foreign key (MenuDetailsId) references MenuDetails(Id),
					QuantityOfOrder int,
					totalPrice money,
					OrderStatus varchar(30));
select * from Orders

create table OrderDetails(Id int primary key not null identity(1,1),
						  OrdersId int foreign key (OrdersId) references Orders(Id),
						  MenuDetailsId int foreign key (MenuDetailsId) references MenuDetails(Id),
						  QuantityOfOrder int); 

/*create table Hotel (Id int primary key not null identity(1,1), 
					HotelName varchar(30),
					ApprovedUsersId int foreign key(ApprovedUsersId) references ApprovedUsers(Id));

create table HotelBranch (Id int primary key not null identity(1,1), 
						  BranchName varchar(30),
						  BranchLocation varchar(30),
						  BranchPhoneNumber varchar(20),
						  HotelId int foreign key(HotelId) references Hotel(Id),
						  ApprovedUsersId int foreign key(ApprovedUsersId) references ApprovedUsers(Id));

create table Orders(Id int primary key not null identity(1,1),
					ApprovedUsersId int foreign key (ApprovedUsersId) references ApprovedUsers(Id),
					HotelId int foreign key (HotelId) references Hotel(Id),
					HotelBranchId int foreign key (HotelBranchId) references HotelBranch(Id),
					OrderStatus varchar(30));

create table HotelBranch (Id int primary key not null identity(1,1), 
						  BranchName varchar(30),
						  BranchLocation varchar(30),
						  BranchPhoneNumber varchar(20),
						  ApprovedUsersId int foreign key(ApprovedUsersId) references ApprovedUsers(Id));


create table MenuDetails (Id int primary key not null identity(1,1),
						  MenuItems varchar(50),
						  MenuQuantity int,
						  Price money,
						  HotelBranchId int foreign key (HotelBranchId) references HotelBranch(Id));


 
create table Orders(Id int primary key not null identity(1,1),
					ApprovedUsersId int foreign key (ApprovedUsersId) references ApprovedUsers(Id),
					HotelBranchId int foreign key (HotelBranchId) references HotelBranch(Id),
					OrderStatus varchar(30));

create table OrderDetails(Id int primary key not null identity(1,1),
						  OrdersId int foreign key (OrdersId) references Orders(Id),
						  MenuDetailsId int foreign key (MenuDetailsId) references MenuDetails(Id),
						  QuantityOfOrder int); */

						  

--------------------------------------------------Approved User Details ----------------------------------------------------------------------------
select * from ApprovedUsers
/*insert approved users*/
create proc ApproveUsersList (
					 @username varchar(30),
					 @email nvarchar(30),
					 @userPassword nvarchar(50),
					 @userRole varchar(15), 
					 @userPhone varchar(20), 
					 @userLocation nvarchar(30),
					 @tempPass nvarchar(10))
as
	begin
	insert into ApprovedUsers values(@username,@email,@userPassword,@userRole,@userPhone,@userLocation,@tempPass)
end


exec ApproveUsersList @username = 'senthil',@email = 'senthil1@gmail.com', @userPassword = 'Senthil1@12345',@userRole = 'User',@userPhone = 9837673560,@userLocation='chennai karpakkam',@tempPass = '';

/*delete approved user*/
create proc DeleteApprovedUSers (@approvedId int, @email varchar(50))
as
	begin
	delete from ApprovedUsers where Id = @approvedId and Email = @email
end

exec DeleteApprovedUSers @email = 'balanm8014@gmail.com', @approvedId = 8

/*update approved user details */
create proc UpdateUserProfile (@id int,@username varchar(30),@userphone varchar(30),@userLocation varchar(30),@email nvarchar(30))
as
	begin
	update ApprovedUsers set UserName = @username,UserPhone = @userphone,UserLocation = @userLocation where Id = @id and Email = @email
end

exec UpdateUserProfile @username = 'senthilk',@userphone='2343232390',@userLocation='coimbatore',@id=4,@email = 'senthil1@gmail.com'

/*read approved users*/
create proc OverallUser
as	
	begin
	select * from ApprovedUsers;
end

exec OverallUser

/*read only users*/
create proc ApprovesOnlyUsers
as
	begin
	select * from ApprovedUsers where UserRole = 'User'
end

exec ApprovesOnlyUsers
/*read only vendor*/
create proc ApprovedOnlyVendors
as
	begin
	select * from ApprovedUsers where UserRole = 'Vendor'
end

exec ApprovedOnlyVendors

/*read the neccessary details for login*/

alter proc GetLoginCredentialsUser (@email varchar(30))
as
	begin
		SELECT UserName,Email,UserRole,Id,UserPassword,TempPassword FROM ApprovedUsers WHERE Email = @email
	end

exec GetLoginCredentialsUser @email = 'rocky143360@gmail.com'



create proc updatePass @email nvarchar(30),@id int, @oldpass nvarchar(10),@newpass nvarchar(50) 
as
	begin
	update ApprovedUsers set UserPassword = @newpass , TempPassword = 'Nov@!U&' where TempPassword = @oldpass and Email = @email and Id = @id
end

exec updatePass  @email='keerthanar310502@gmail.com',@id=10,@oldpass='qLTibh3CWE',@newpass='Keerthana@12345'


-------------------------------------------------- Pending User Details----------------------------------------------------------------------------
/*overall pending user*/
select * from PendingUser;


/* insert pending user */
create proc PendingUsersList (
					 @username varchar(30),
					 @email nvarchar(30),
					 @password nvarchar(50),
					 @userRole varchar(15), 
					 @userPhone varchar(20), 
					 @userLocation nvarchar(30),
					 @temppass nvarchar(10))
as
	begin
	insert into PendingUser values(@username,@email,'','User',@userPhone,@userLocation,'')
end

exec PendingUsersList @username = 'senthil',@email = 'senthil1@gmail.com',@userRole = 'User',@userPhone = 9837673560,@userLocation='chennai karpakkam';


/*delete for both Pending users */
create proc DeletePendingUSers (@pendingId int)
as
	begin
	delete from PendingUser where Id = @pendingId
end

exec DeletePendingUSers @email = 'senthil1@gmail.com', @pendingId = 2


/*read pending user*/
create proc PendingallUser
as	
	begin
	select * from PendingUser where UserRole = 'User';
end

exec PendingallUser

-------------------------------------------------- Pending vendor details ----------------------------------------------------------------------------

/* insert vendor details*/

alter proc PendingVendorList (
					 @username varchar(30),
					 @email nvarchar(30),
					 @password nvarchar(50),
					 @userRole varchar(15), 
					 @userPhone varchar(20), 
					 @userLocation nvarchar(30),
					 @temppass nvarchar(10))
as
	begin
	insert into PendingUser values(@username,@email,'','Vendor',@userPhone,@userLocation,'')
end

	exec PendingVendorList @username = 'senthil',@email = 'senthil1@gmail.com',@password='',@userRole = '',@userPhone = 9837673560,@userLocation='chennai karpakkam',@temppass='';

/*delete Pending vendor*/
create proc DeletePendingVendors (@pendingId int)
as
	begin
	delete from PendingUser where Id = @pendingId
end

exec DeletePendingVendors @email = 'senthil1@gmail.com', @pendingId = 5

/*read pending vendor*/
create proc PendingAllVendor
as	
	begin
	select * from PendingUser where UserRole = 'Vendor';
end

exec PendingAllVendor

-------------------------------------------------------Hotel-----------------------------------
select * from Hotel;
/*add hotel*/
alter PROCEDURE InsertHotel
    @HotelName VARCHAR(30),
    @ApprovedUsersId INT
AS
BEGIN
    INSERT INTO Hotel (HotelName, ApprovedUsersId)
    VALUES (@HotelName, @ApprovedUsersId);
END;

exec InsertHotel @HotelName='KFC',@ApprovedUsersId=1

/* delete hotel
alter PROCEDURE DeleteHotel
    @HotelId INT,
    @AdminRole NVARCHAR(30)
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Hotel H
        JOIN ApprovedUsers AU ON H.ApprovedUsersId = AU.Id
        WHERE H.Id = @HotelId AND AU.UserRole = 'Admin'
    )
    BEGIN
        DELETE FROM Hotel
        WHERE Id = @HotelId;
    END
END;


exec DeleteHotel @HotelId=8,@AdminRole='User'*/
ALTER PROCEDURE DeleteHotel
    @HotelId INT,
    @AdminRole NVARCHAR(30)
AS
BEGIN
    IF @AdminRole = 'Admin'
    BEGIN
        DELETE FROM Hotel
        WHERE Id = @HotelId;
    END
END;

EXEC DeleteHotel @HotelId = 7, @AdminRole = 'User';

/*update hotel*/
alter proc Alterhotel @hotelid int,@hotelname varchar(30),@approveduserId int
as
	begin
		update Hotel set HotelName = @hotelname from Hotel H join ApprovedUsers AU on H.ApprovedUsersId = AU.Id where H.Id = @hotelid and H.ApprovedUsersId = @approveduserId and AU.UserRole = 'Admin'
end

exec Alterhotel @hotelid = 5,@hotelname='Aambur',@approveduserId =3

		


/*read ALL hotel*/
alter proc GetallHotel
as
	begin
	select * from Hotel;
end

exec GetallHotel
-------------------------------------------------- Branch Details ----------------------------------------------------------------------------

/*show all branches including vendor as well as admin*/
select * from HotelBranch;

SELECT COUNT(*) FROM HotelBranch WHERE BranchName = @branchName AND HotelId = @hotelId

/*insert branch*/
create proc AddHotelBranch(
						    @branchName varchar(30),
						  @branchLocation varchar(30),
						  @branchPhoneNumber varchar(20),
						  @hotelId int 
						  )
as
	begin
	insert into HotelBranch values (@branchName,@branchLocation,@branchPhoneNumber,@hotelId);
end

exec AddHotelBranch @branchName='branch1',@branchLocation='chennai shollinganallur',@branchPhoneNumber = 9876543221,@hotelId = 4



/*update hotel branch by vendor creation*/
create proc UpdateBranchByVendor (@branchId int,@branchname varchar(30),@branchLocation varchar(30),@branchPhoneNumber varchar(20),@branchOwnerId int)
as
	begin
	update HotelBranch set BranchName = @branchname,BranchLocation = @branchLocation,BranchPhoneNumber = @branchPhoneNumber from HotelBranch HB join ApprovedUsers AU on HB.ApprovedUsersId = AU.Id where HB.Id = @branchId and AU.Id = @branchOwnerId and AU.UserRole = 'Vendor'
end

exec UpdateBranchByVendor @branchname='Changingbranch2',@branchLocation='changing branch2 location',@branchPhoneNumber='7366637370', @branchId = 2 , @branchOwnerId = 4

/*update hotel branch by Admin creation*/

alter proc UpdateBranchByAdmin(
								@branchId int,
								@branchname varchar(30),
								@branchLocation varchar(30),
								@branchPhoneNumber varchar(20),
								@hotelId int)
as
	begin
	update HotelBranch set BranchName = @branchname,BranchLocation = @branchLocation,BranchPhoneNumber = @branchPhoneNumber 
						from HotelBranch HB join Hotel H on HB.HotelId = H.Id join ApprovedUsers AU on H.ApprovedUsersId =AU.Id
						where AU.UserRole = 'Admin' and HB.Id = @branchId and HB.HotelId = @hotelId
end

exec UpdateBranchByAdmin @branchname='Changingbranch1',@branchLocation='changing branch location',@branchPhoneNumber='12345678901', @branchId = 3 , @hotelId = 7


/*Read specific Hotelbranch*/
/*create proc ListAllBranches @id int, @role varchar(30)
as
	begin
	select * from HotelBranch HB join ApprovedUsers AU on HB.ApprovedUsersId = AU.Id  where HB.ApprovedUsersId = @id and AU.UserRole = @role ;
end

exec ListAllBranches @id = 2,@role = 'Admin'
*/

/*read admin by all branches*/
create proc ListAllBranches
as
	begin
	select * from HotelBranch;
end

exec ListAllBranches

/* Delete hotel branch by admin*/
alter PROCEDURE DeleteApprovedHotelBranch 
    @id INT,
    @hotelId INT
AS
BEGIN
    DELETE HB
    FROM HotelBranch HB
     JOIN Hotel H ON HB.HotelId = H.Id
    WHERE  HB.Id = @id and H.Id = @hotelId ;
END;

exec DeleteApprovedHotelBranch @id = 2,@hotelId=2

create proc 

-------------------------------------------------------------Menu --------------------------------------------------------
select * from MenuDetails

/*insert menu*/
create proc AddMenuItems @menuName varchar(50),@menuItemQuantity int,@menuPrice money,@hotelBranchId int
as
	begin
	insert into MenuDetails values (@menuName,@menuItemQuantity,@menuPrice,@hotelBranchId)
end

exec AddMenuItems @menuName='Pizza',@menuItemQuantity=1,@menuPrice=120,@hotelBranchId=1

/*update menu*/
create proc UpdatemenuDetails @itemId int, @menuName varchar(50),@menuItemQuantity int,@menuItemPrice money,@hotelBranchId int
as
	begin
	update MenuDetails set MenuItems = @menuName,MenuQuantity = @menuItemQuantity,Price = @menuItemPrice 
	from MenuDetails MD join HotelBranch HB on MD.HotelBranchId = HB.Id  
	join Hotel H on HB.HotelId = H.Id 
	join ApprovedUsers AU on H.ApprovedUsersId = AU.Id where MD.Id = @itemId and   HB.Id = @hotelBranchId

end

exec UpdatemenuDetails

/*delete menu once more check*/

CREATE PROCEDURE DeleteMenuItems 
    @menuId INT,
    @hotelbranchId INT
AS
BEGIN
    DELETE MD
    FROM MenuDetails MD
    JOIN HotelBranch HB ON MD.HotelBranchId = HB.Id
    WHERE MD.Id = @menuId AND HB.Id = @hotelbranchId;
END;

exec DeleteMenuItems @menuId =33 ,@hotelbranchId=2

/*read menu by admin to modify view*/
create proc viewMenuItems
as
	begin
	select * from MenuDetails
end

exec viewMenuItems

/*read menu from the user*/
alter proc ShowMenuDetail
as
	begin
	SELECT MenuDetails.Id, MenuDetails.MenuItems,MenuDetails.Price, HotelBranch.BranchName,HotelBranch.BranchLocation,MenuDetails.HotelBranchId
	FROM MenuDetails
	JOIN HotelBranch ON MenuDetails.HotelBranchId = HotelBranch.Id;
end
exec ShowMenuDetail


alter proc filterMenu @menuItems varchar(50),@branchLocation varchar(30)
as
	begin 
	select MenuDetails.Id, MenuDetails.MenuItems,MenuDetails.Price, HotelBranch.BranchName,HotelBranch.BranchLocation,MenuDetails.HotelBranchId
	from MenuDetails
	join HotelBranch on MenuDetails.HotelBranchId = HotelBranch.Id where MenuDetails.MenuItems = @menuItems and  HotelBranch.BranchLocation= @branchLocation;
end

exec filterMenu @menuItems='Pizza', @branchLocation='Karpakam'

select * from HotelBranch
select * from MenuDetails
select * from ApprovedUsers

select * from Orders
/*pending Order*/
alter proc UsersOrders @userId int
as
	begin
	select MenuDetails.Id,Orders.QuantityOfOrder,Orders.OrderStatus,Orders.TotalPrice,MenuDetails.MenuItems from Orders
	join MenuDetails on Orders.MenuDetailsId = MenuDetails.Id
	join HotelBranch on Orders.HotelBranchId = HotelBranch.Id
	join ApprovedUsers on Orders.ApprovedUsersId = ApprovedUsers.Id
	where ApprovedUsers.Id = @userId and Orders.OrderStatus = 'Pending' 
end

exec UsersOrders @userId =11

select * from ApprovedUsers
/*approved order list by specific user*/
alter proc ApprovedUsersOrders @userId int
as
	begin
	select MenuDetails.Id,Orders.QuantityOfOrder,Orders.OrderStatus,Orders.TotalPrice,MenuDetails.MenuItems from Orders
	join MenuDetails on Orders.MenuDetailsId = MenuDetails.Id
	join HotelBranch on Orders.HotelBranchId = HotelBranch.Id
	join ApprovedUsers on Orders.ApprovedUsersId = ApprovedUsers.Id
	where ApprovedUsers.Id = @userId and Orders.OrderStatus = 'Approved' 
end

exec ApprovedUsersOrders @userId =11



/*update the order status to approved
alter proc ChangeOrderApproved @userRole nvarchar(30), @orderId int
as
	begin
	update Orders set OrderStatus='Approved' from Orders 
	join MenuDetails on Orders.MenuDetailsId = MenuDetails.Id
	join HotelBranch on Orders.HotelBranchId = HotelBranch.Id
	join ApprovedUsers on Orders.ApprovedUsersId = ApprovedUsers.Id
	/*where ApprovedUsers.Id = @userId and ((ApprovedUsers.UserRole = 'Admin')or(ApprovedUsers.UserRole = 'Vendor')) and Orders.Id = @orderId*/
	where ApprovedUsers.UserRole = @userRole and Orders.Id = @orderId
end*/

alter proc ChangeOrderApproved @userRole nvarchar(30), @orderId int
as
	begin
	update Orders set OrderStatus='Approved' from Orders join ApprovedUsers on ApprovedUsers.UserRole = 'Admin'
	where ApprovedUsers.UserRole = @userRole and Orders.Id = @orderId
end

select * from ApprovedUsers
select * from Orders where Id =8
exec ChangeOrderApproved @userRole = 'Admin',@orderId=8



-------------------------------------------------------------Order --------------------------------------------------------

alter proc AddOrder @userId int,@hotelbracnhId int,@menudetailId int,@quantityCount int,@totalPrice money, @orderStatus varchar(30)
as
	begin
	insert into Orders values (@userId,@hotelbracnhId,@menudetailId,@quantityCount,@totalPrice,'Pending')
end
exec AddOrder





------ omit-----
create proc AddOrderDetails @OrdersId int,@MenuDetailsId int,@QuantityOfOrder int
as
	begin
	insert into OrderDetails values (@OrdersId,@MenuDetailsId,@QuantityOfOrder)
end

exec AddOrderDetails

create proc getOrderId 
as
	begin 
	select MAX(Id) from Orders
end

exec getOrderId
----------------------
-------------------------------------------------------------Order Detail--------------------------------------------------------


-------------------------------------------only counts--------------------------------
/*no of hotel*/
select count(*) as TotalHotel from Hotel;
/*no of branch*/
select count(*) as TotalBranch from HotelBranch
/*no of approved user*/
select count(*) as ApprovedUser from ApprovedUsers where UserRole = 'User'
/*no of approved vendor*/
select count(*) as ApprovedUser from ApprovedUsers where UserRole = 'Vendor'
/*no of pending user*/
select count(*) as PendingUser from PendingUser where UserRole = 'User'
/*no of pending vendor*/
select count(*) as PendingVendor from PendingUser where UserRole = 'User'


/*new arrivals*/


create proc GetNewArrivals
as
	begin
	select UserName, UserRole, Email from ApprovedUsers where UserRole IN ('User', 'Vendor') group by UserName, UserRole, Email order by max(Id) desc;
end

exec GetNewArrivals


/*approve order by admin*/

/*alter proc ApporoveOrder @orderId int,@userRole nvarchar(30)
as
	begin
	update Orders set Orders.OrderStatus = 'Approved' from Orders
	join MenuDetails on Orders.MenuDetailsId = MenuDetails.Id
	join HotelBranch on Orders.HotelBranchId = HotelBranch.Id
	join Hotel on HotelBranch.HotelId = Hotel.Id
	join ApprovedUsers on Hotel.ApprovedUsersId = ApprovedUsers.Id
	where ApprovedUsers.UserRole = (@userRole = 'Admin') and Orders.Id = @orderId

end

exec ApporoveOrder @orderId=18,@userRole = 'User'*/

ALTER PROCEDURE ApporoveOrder 
    @orderId INT,
    @userRole NVARCHAR(30)
AS
BEGIN
    IF @userRole = 'Admin'
    BEGIN
        UPDATE Orders
        SET OrderStatus = 'Approved'
        FROM Orders
        JOIN MenuDetails ON Orders.MenuDetailsId = MenuDetails.Id
        JOIN HotelBranch ON Orders.HotelBranchId = HotelBranch.Id
        JOIN Hotel ON HotelBranch.HotelId = Hotel.Id
        JOIN ApprovedUsers ON Hotel.ApprovedUsersId = ApprovedUsers.Id
        WHERE Orders.Id = @orderId;
    END
    ELSE
    BEGIN
        SELECT 'Invalid user. Only Admin can approve orders.' AS ErrorMessage;
    END
END

exec ApporoveOrder @orderId=19,@userRole = 'Admin'

/*get email*/
alter proc getEmail @orderID int
as
	begin
	select ApprovedUsers.Email from ApprovedUsers 
	join Orders on Orders.ApprovedUsersId = ApprovedUsers.Id
	WHERE Orders.Id = @orderID;
end

exec getEmail @orderID = 5


select * from Orders

select * from ApprovedUsers


alter proc sendemail 
as
	begin
	select Orders.Id,MenuDetails.MenuItems,ApprovedUsers.Email,HotelBranch.BranchName,Orders.QuantityOfOrder,Orders.totalPrice,Orders.OrderStatus
	from Orders join MenuDetails on Orders.MenuDetailsId = MenuDetails.Id
	join HotelBranch on Orders.HotelBranchId = HotelBranch.Id
	join ApprovedUsers on Orders.ApprovedUsersId = ApprovedUsers.Id
end

exec sendemail

select * from PendingUser
