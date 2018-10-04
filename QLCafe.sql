CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
GO

-- Food
-- Table
-- FoodCategory
-- Account
-- Bill
-- BillInfo

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán
	
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO

INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'K9' , -- UserName - nvarchar(100)
          N'RongK9' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          1  -- Type - int
        )
INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWord ,
          Type
        )
VALUES  ( N'staff' , -- UserName - nvarchar(100)
          N'staff' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          0  -- Type - int
        )
GO

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO

--EXEC dbo.USP_GetAccountByUserName @userName = N'K9' -- nvarchar(100)
GO 
-- Lấy thông tin để đăng nhập
CREATE PROC USP_Login
	@userName  nvarchar(100),
	@passWord  nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END

GO
--EXECUTE dbo.USP_Login @userName , @passWord 

-- insert giá trị cho bảng Table

DECLARE @i INT = 0
WHILE @i <=10
BEGIN
	INSERT INTO dbo.TableFood (name) VALUES (N'Bàn ' + CAST(@i AS nvarchar(100))) 
	SET @i = @i + 1
END

SELECT * FROM dbo.TableFood 

-- lấy tất cả các bàn đang có
GO

CREATE PROC USP_getTableList
AS
BEGIN
	SELECT * FROM dbo.TableFood
END
GO
--EXECUTE dbo.USP_getTableList
GO
UPDATE dbo.TableFood SET status = N'Có Người' WHERE name = N'Bàn 6'
GO
-- Nhập dữ liệu cho Bill và BillInfo , Food , Category
-- thêm category
INSERT INTO dbo.FoodCategory 
	( name)
	VALUES (N'Hải Sản')

INSERT INTO dbo.FoodCategory 
	( name)
	VALUES (N'Giải Khát')

INSERT INTO dbo.FoodCategory 
	( name)
	VALUES (N'Ăn Nhẹ')

INSERT INTO dbo.FoodCategory 
	( name)
	VALUES (N'Trái Cây')

SELECT * FROM dbo.FoodCategory
GO
-- thêm món ăn
INSERT INTO dbo.Food
	(idCategory , name , price)
	VALUES (1 , N'Tôm Hùm Nướng Bơ Tỏi' , 500000)

INSERT INTO dbo.Food
	(idCategory , name , price)
	VALUES (2 , N'7 Up' , 15000)

INSERT INTO dbo.Food
	(idCategory , name , price)
	VALUES (3 , N'Khoai Tây Chiên' , 20000)

INSERT INTO dbo.Food
	(idCategory , name , price)
	VALUES (4 , N'Sầu Riêng' , 30000)

SELECT * FROM dbo.Food
GO
--thêm Bill
INSERT INTO dbo.Bill
	(DateCheckIn , DateCheckOut , idTable , status)
	VALUES (GETDATE(), NULL,1,0)

INSERT INTO dbo.Bill
	(DateCheckIn , DateCheckOut , idTable , status)
	VALUES (GETDATE(), GETDATE(),3,1)

INSERT INTO dbo.Bill
	(DateCheckIn , DateCheckOut , idTable , status)
	VALUES (GETDATE(), NULL,7,0)

INSERT INTO dbo.Bill
	(DateCheckIn , DateCheckOut , idTable , status)
	VALUES (GETDATE(), NULL,4,0)

SELECT * FROM dbo.Bill
GO
--thêm BillInfo
INSERT INTO dbo.BillInfo
	(idBill,idFood,count)
	VALUES (1, 3, 5)

INSERT INTO dbo.BillInfo
	(idBill,idFood,count)
	VALUES (2, 2, 1)

INSERT INTO dbo.BillInfo
	(idBill,idFood,count)
	VALUES (3, 1, 6)

INSERT INTO dbo.BillInfo
	(idBill,idFood,count)
	VALUES (4, 4, 5)

SELECT * FROM dbo.BillInfo
GO
-- Lấy  Bill từ Table ID
CREATE PROC USP_getUnCheckOutBillIDByTableID
@tableID int 
AS
BEGIN
	SELECT * FROM dbo.Bill WHERE idTable = @tableID AND status = 0
END
Go
--EXECUTE dbo.USP_getUnCheckOutBillIDByTableID @tableID = 1
GO
--Lấy Bill Info từ ID Bill
CREATE PROC USP_getBillInFoByIDBill
@idBill int
AS
BEGIN
	SELECT * FROM dbo.BillInfo WHERE idBill = @idBill
END
Go
--EXECUTE dbo.USP_getBillInFoByIDBill @idBill = 1
GO
-- Lấy dữ liệu cho Menu (foodName , count) theo id table
CREATE PROC USP_getMenuByIdTable
@idTable int 
AS
BEGIN
	SELECT dbo.Food.name , dbo.BillInfo.count ,dbo.Food.price, dbo.Food.price*dbo.BillInfo.count as totalPrice 
	FROM (dbo.Food JOIN dbo.BillInfo ON dbo.Food.id = dbo.BillInfo.idFood) JOIN dbo.Bill ON dbo.BillInfo.idBill = dbo.Bill.id
	WHERE dbo.Bill.idTable = @idTable AND dbo.Bill.status = 0
END
GO
--EXECUTE dbo.USP_getMenuByIdTable @idTable = 1

GO
-- Lấy Category
CREATE PROC USP_getListCategory
AS
BEGIN
	SELECT * FROM dbo.FoodCategory
END
Go
--EXECUTE dbo.USP_getListCategory
GO

-- Lấy List Food theo ID category
CREATE PROC USP_getListFoodByCategoryId
@idCategory int 
AS
BEGIN
	SELECT * FROM dbo.Food WHERE idCategory = @idCategory
END
GO
--EXECUTE dbo.USP_getListFoodByCategoryId @idCategory = 1
GO
-- thêm trường discount cho bill
ALTER TABLE dbo.Bill
ADD discount INT 
GO
ALTER TABLE dbo.Bill
ADD sumprice FLOAT
GO
-- Insert Bill mới từ phần mềm
CREATE PROC USP_insertBill
@idTable int


AS
BEGIN
	INSERT INTO dbo.Bill (idTable,DateCheckOut , discount, sumprice)
	VALUES (@idTable,NULL,0, 0)
END
GO
--EXECUTE dbo.USP_insertBill @idTable  

GO
--Lấy id max của Bill
CREATE PROC USP_getMaxIdBill
AS
BEGIN
	SELECT MAX(id) FROM dbo.Bill
END
GO
--EXECUTE dbo.USP_getMaxIdBill
GO
-- Insert BillInFo từ phần mềm có 2 trường hợp cùng 1 idbill nhưng món không trùng
-- và có món trùng thì cập nhật lại count
CREATE PROC USP_insertBillInFo
@idBill int,
@idFood int,
@count int
AS
BEGIN
	DECLARE @isExistBillInfo INT
	DECLARE @foodCount INT = 1

	SELECT @isExistBillInfo = id , @foodCount = count 
	FROM dbo.BillInfo
	WHERE idBill = @idBill AND idFood = @idFood

	IF(@isExistBillInfo > 0)
		BEGIN
			DECLARE @newCount INT = @foodCount + @count
			IF(@newCount > 0)
				UPDATE dbo.BillInfo SET count = @newCount WHERE idFood = @idFood
			ELSE
				DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
		END
	ELSE
	BEGIN
		INSERT INTO dbo.BillInfo (idBill , idFood , count)
		VALUES (@idBill , @idFood , @count)
	END
END
GO
--EXECUTE dbo.USP_insertBillInFo @idBill , @idFood , @count
Go
-- Checkout bill
CREATE PROC USP_CheckOutBill
@idBill INT ,
@discount INT , 
@sumprice FLOAT
AS
BEGIN
	UPDATE dbo.Bill SET status = 1  , DateCheckOut = GETDATE() , discount = @discount , sumprice = @sumprice
					WHERE id = @idBill
END
GO
--EXECUTE dbo.USP_CheckOutBill @idBill , @discount , @sumprice
GO
-- cập nhật status  của bàn
Go
CREATE trigger UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = idBill FROM inserted --lấy id Bill từ BillInfo khi ta thêm món

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0 -- lấy id Table của cái Bill chưa checkout dựa theo id Bill mới thêm món
	
	DECLARE @count INT -- số món của bill đó
	SELECT @count = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill
	IF(@count > 0)
		UPDATE dbo.TableFood SET status = N'Có Người' WHERE id = @idTable
	ELSE
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable

END
GO

CREATE trigger UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM inserted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill 
	DECLARE @count INT
	SELECT @count = COUNT(*) FROM dbo.Bill  WHERE idTable = @idTable AND status = 0
	if(@count = 0)
	BEGIN
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
	END
END
GO


UPDATE dbo.Bill SET discount =0 , sumprice =0

-- chuyển bàn
GO 
-- đổi bàn
CREATE PROC USP_SwitchTable
@idTable1 INT , 
@idTable2 INT

AS
BEGIN
	DECLARE @count1 INT 
	DECLARE @count2 INT
	DECLARE @idBill1 INT
	DECLARE @idBill2 INT
	-- lấy idBill1 
	-- lấy số  bill ứng với bàn  1
	SELECT @count1 = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable1  AND status = 0
	IF(@count1 = 0) -- tức là k có bill 
		BEGIN
			INSERT INTO dbo.Bill(DateCheckIn,DateCheckOut,idTable,discount,sumprice) VALUES(GETDATE(),NULL,@idTable1,0,0)
			SELECT @idBill1 = MAX(id) FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0
		END
	ELSE --là có bill rồi
		BEGIN
			SELECT @idBill1 = id FROM dbo.Bill WHERE  idTable = @idTable1  AND status = 0
		END

		-- lấy idBill2 
	-- lấy số  bill ứng với bàn  2
	SELECT @count2 = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
	IF(@count2 = 0) -- tức là k có bill 
		BEGIN
			INSERT INTO dbo.Bill(DateCheckIn,DateCheckOut,idTable,discount,sumprice) VALUES(GETDATE(),NULL,@idTable2,0,0)
			SELECT @idBill2 = MAX(id) FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
		END
	ELSE --là có bill rồi
		BEGIN
			SELECT @idBill2 = id FROM dbo.Bill WHERE @idTable2 = idTable AND status = 0
		END

	-- gán danh sách các idBillInfo của idBill1 vào bảng tạm
	SELECT id INTO IdBillInfoTable FROM BillInfo WHERE idBill = @idBill1

	--gán danh sách idBill của idBill2 trong bảng BillInfo là idBill1
	UPDATE dbo.BillInfo SET idBill = @idBill1 WHERE idBill = @idBill2

	--gán danh sách idBill của idBill1 trong bảng BillInfo là idBill2
	UPDATE dbo.BillInfo SET idBill = @idBill2 WHERE id IN ( SELECT * FROM IdBillInfoTable)

	DROP TABLE IdBillInfoTable
	if(@count1 = 0 ) -- ban đầu là k có  thì khi chuyển sẽ có
		UPDATE dbo.Bill SET status = 1 WHERE id = @idBill2
	if(@count2 = 0)
		UPDATE dbo.Bill SET status = 1 WHERE id = @idBill1
END
Go 
--EXECUTE dbo.USP_SwitchTable @idTable1 , @idTable2
GO
-- lấy danh sách hóa đơn đã thanh toán
CREATE PROC USP_getListBillCheckOut
@dateStart date,
@dateEnd date
AS
BEGIN
	SELECT TableFood.name as [Tên Bàn] , DateCheckIn as[Ngày Vào] , DateCheckOut as[Ngày Ra] , discount[Giảm Giá] , sumprice as [Tổng Tiền] 
	FROM dbo.Bill JOIN dbo.TableFood ON Bill.idTable = TableFood.id
	WHERE Bill.status = 1  AND DateCheckIn >= @dateStart AND DateCheckOut <= @dateEnd
END
GO
--DECLARE @dateStart date = GETDATE()
--DECLARE @dateEnd date  = GETDATE()
--EXECUTE dbo.USP_getListBillCheckOut @dateStart   ,  @dateEnd 
GO
-- Update account
CREATE PROC USP_UpdateAccount
@userName nvarchar(100),
@displayName nvarchar(100),
@password nvarchar(100),
@newpassword nvarchar(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE PassWord = @password AND UserName = @userName
	IF(@isRightPass = 1)
		BEGIN
			IF(@newpassword IS NULL OR @newpassword = '')
				BEGIN
					UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
				END
			ELSE
				BEGIN
					UPDATE dbo.Account SET DisplayName = @displayName , PassWord = @newpassword
										WHERE UserName = @userName
				END
		END
END
Go
--EXECUTE dbo.USP_UpdateAccount @userName , @displayName , @password , @newpassword
Go
-- lấy danh sách thức ăn
CREATE PROC USP_getListFood
AS
BEGIN
	SELECT dbo.Food.id , dbo.Food.name , dbo.FoodCategory.name as [namecategory] , dbo.Food.price 
	FROM dbo.Food JOIN dbo.FoodCategory ON dbo.Food.idCategory = dbo.FoodCategory.id 
	
END
GO 
--EXECUTE dbo.USP_getListFood
GO
-- thêm món ăn
CREATE PROC USP_addFood
@name nvarchar(100),
@idcategory int,
@price float
AS
BEGIN
		 

		 INSERT INTO dbo.Food (name,idCategory,price) VALUES (@name , @idcategory , @price)
END
GO
--EXECUTE dbo.USP_addFood @name , @idcategory , @price
Go
-- sửa món ăn
CREATE PROC USP_updateFood
@id int,
@name nvarchar(100),
@idcategory int,
@price float
AS
BEGIN

		 UPDATE dbo.Food SET name = @name , idCategory = @idcategory , price = @price
						WHERE id = @id
END
GO
--EXECUTE dbo.USP_updateFood @id , @name , @idcategory , @price
GO 
-- xóa món ăn
CREATE PROC USP_deleteBillInfoByidFood
@idfood int
AS
BEGIN
	DELETE dbo.BillInfo WHERE idFood = @idfood
END
GO
--EXECUTE dbo.USP_deleteBillInfoByidFood @idfood
GO

CREATE PROC USP_deleteFood
@idfood int
AS
BEGIN
	DELETE dbo.Food WHERE id = @idfood
END
GO
--EXECUTE dbo.USP_deleteFood @idfood
GO
--Lấy danh sách thức ăn theo tiềm kiếm
-- hàm biến các ký tự có dấu thành k dấu
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
CREATE PROC USP_getListFoodBySearching
@nameFood nvarchar(100)
AS
BEGIN
	SELECT dbo.Food.id , dbo.Food.name , dbo.FoodCategory.name as [namecategory] , dbo.Food.price 
	FROM dbo.Food JOIN dbo.FoodCategory ON dbo.Food.idCategory = dbo.FoodCategory.id
	WHERE dbo.fuConvertToUnsign1(dbo.Food.name) LIKE N'%'+dbo.fuConvertToUnsign1(@nameFood)+'%'
END
GO
--EXECUTE dbo.USP_getListFoodBySearching @nameFood
Go
-- lấy danh sách các tài khoản
CREATE PROC USP_getListAccount
AS
BEGIN
	SELECT * FROM dbo.Account
END
GO
--EXECUTE dbo.USP_getListAccount
Go
-- Thêm account
CREATE PROC USP_addAccount
@username nvarchar(100),
@displayname nvarchar(100),
@type int
AS
BEGIN
	INSERT INTO dbo.Account (UserName,DisplayName,Type) 
	VALUES (@username,@displayname,@type)
END
GO
--EXECUTE dbo.USP_addAccount @username , @displayname , @type

-- Update Account
CREATE PROC USP_updateAccountByAdmin
@username nvarchar(100),
@displayname nvarchar(100),
@type int
AS
BEGIN
	UPDATE  dbo.Account  SET DisplayName = @displayname , Type = @type
							WHERE UserName = @username 
	
END
GO
--EXECUTE dbo.USP_updateAccountByAdmin @username , @displayname , @type
GO
-- Xóa tài khoản
CREATE PROC USP_deleteAccount
@username nvarchar(100)
AS
BEGIN
	DELETE  dbo.Account	WHERE UserName = @username 
	
END
GO
--EXECUTE dbo.USP_deleteAccount @username
GO
-- đặt lại mật khẩu
CREATE PROC USP_datlaiMk
@username nvarchar(100)
AS
BEGIN
	UPDATE dbo.Account SET PassWord = 0 WHERE UserName  =@username
	
END
GO
--EXECUTE dbo.USP_datlaiMk @username
GO

-- Thêm Bàn
CREATE PROC USP_addTable
@nametable nvarchar(100)
AS
BEGIN
	INSERT INTO dbo.TableFood (name) VALUES (@nametable)
	
END
GO
--EXECUTE dbo.USP_addTable @nametable
GO
--Update bàn
CREATE PROC USP_updateTable
@id INT,
@nametable nvarchar(100)
AS
BEGIN
	UPDATE dbo.TableFood SET name = @nametable WHERE id = @id
	
END
GO
--EXECUTE dbo.USP_updateTable @id  ,@nametable 
GO
--Xóa bàn
--Xóa billinfo chứa bill có chứa id bàn
CREATE PROC USP_deleteBillInfoByIdTable
@idtable int
AS
BEGIN
	SELECT id INTO IdBillInfoBill
	FROM dbo.Bill
	WHERE idTable = @idtable

	DELETE dbo.BillInfo WHERE idBill IN (SELECT * FROM IdBillInfoBill)
	DROP TABLE IdBillInfoBill
END
GO
--EXECUTE dbo.USP_deleteBillInfoByIdTable @idtable
Go
-- Xóa Bill chứa id bàn đó trước
CREATE PROC USP_deleteBillByIdTable
@idtable int
AS
BEGIN
	DELETE dbo.Bill WHERE idTable = @idtable
END
GO
--EXECUTE dbo.USP_deleteBillByIdTable @idtable
Go
-- xóa bàn
CREATE PROC USP_deleteTable
@idtable int
AS
BEGIN
	DELETE dbo.TableFood WHERE id = @idtable
END
GO
--EXECUTE dbo.USP_deleteTable @idtable
Go
-- Tìm Bàn
CREATE PROC USP_getListTableBySearching
@name nvarchar(100)
AS
BEGIN
	SELECT *
	FROM dbo.TableFood
	WHERE dbo.fuConvertToUnsign1(name) LIKE '%' + dbo.fuConvertToUnsign1(@name) + '%'
END
GO
--EXECUTE dbo.USP_getListTableBySearching @name
Go
-- Add category
CREATE PROC USP_addCategory
@namecategory nvarchar(100)
AS
BEGIN
	INSERT INTO dbo.FoodCategory (name) VALUES(@namecategory)
END
GO
--EXECUTE dbo.USP_addCategory @namecategory
GO
--Update Category
CREATE PROC USP_updateCategory
@id int,
@namecategory nvarchar(100)
AS
BEGIN
	UPDATE dbo.FoodCategory SET name = @namecategory WHERE id = @id
END
GO
--EXECUTE dbo.USP_updateCategory @id , @namecategory
GO
-- xóa category
-- xóa Billinfo chứa thức ăn có idcategory này
CREATE PROC USP_deleteBillInfoByIdCategory
@idcategory int
AS
BEGIN
	SELECT id  INTO IdBillInfoFood
	FROM dbo.Food
	WHERE idCategory = @idcategory

	DELETE dbo.BillInfo WHERE idFood IN (SELECT * FROM IdBillInfoFood)
	DROP TABLE IdBillInfoFood
END
GO
--EXECUTE dbo.USP_deleteBillInfoByIdCategory @idcategory

-- Xóa món ăn có chứa id category này
CREATE PROC USP_deleteFoodByIdCategory
@idcategory int
AS
BEGIN
	DELETE dbo.Food WHERE idCategory = @idcategory
END
GO
--EXECUTE dbo.USP_deleteFoodByIdCategory @idcategory
-- Xóa Category
CREATE PROC USP_deleteCategory
@idcategory int
AS
BEGIN
	DELETE dbo.FoodCategory WHERE id = @idcategory;
END
GO
--EXECUTE dbo.USP_deleteCategory @idcategory
GO
-- Tìm Category
CREATE PROC USP_getlistCategoryBySearching
@name nvarchar(100)
AS
BEGIN
	SELECT *
	FROM dbo.FoodCategory
	WHERE dbo.fuConvertToUnsign1(name) LIKE '%' + dbo.fuConvertToUnsign1(@name) + '%'
END
GO
--EXECUTE dbo.USP_getlistCategoryBySearching @name
GO
CREATE PROC USP_getReport
@idBill int 
AS
BEGIN
	SELECT dbo.Food.name , dbo.BillInfo.count , dbo.Food.price * dbo.BillInfo.count as [totalprice]
	FROM (dbo.Bill JOIN dbo.BillInfo ON dbo.Bill.id = dbo.BillInfo.idBill) JOIN dbo.Food ON dbo.BillInfo.idFood = dbo.Food.id
	WHERE dbo.Bill.id  = @idBill
END
--test
GO
SELECT * FROM dbo.TableFood
SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
SELECT * FROM dbo.FoodCategory
SELECT * FROM dbo.Account
SELECT * FROM dbo.Food
GO
DELETE dbo.BillInfo
DELETE dbo.Bill



UPDATE dbo.TableFood SET status = N'Trống' WHERE id = 7
