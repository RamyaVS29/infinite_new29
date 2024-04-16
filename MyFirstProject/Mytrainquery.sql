create database MyNewPrjDB
use  MyNewPrjDB

-- Create the Train table
CREATE TABLE Train (
    TrainNumber INT PRIMARY KEY,
    TrainName VARCHAR(100),
    Source VARCHAR(100),
    Destination VARCHAR(100),
    TicketClass VARCHAR(50),
    TicketPrice DECIMAL(10, 2),
    TotalBerths INT,
    AvailableBerths INT,
    Status VARCHAR(20),
    DepartureTime TIME,
    ArrivalTime TIME,
    DaysOfOperation VARCHAR(250),
    Stops INT
);


-- Insert three records into the Train table
INSERT INTO Train (TrainNumber, TrainName, Source, Destination, TicketClass, TicketPrice, TotalBerths, AvailableBerths, Status, DepartureTime, ArrivalTime, DaysOfOperation, Stops)
VALUES 
    (2222, 'KarnatakaExpress', 'Bengaluru', 'Mysore', 'First', 500.00, 100, 50, 'Active', '08:00:00', '10:30:00', 'Monday, Wednesday, Friday', 5),
    (2233, 'Superfast', 'Mysore', 'Mangalore', 'Second', 300.00, 120, 100, 'Inactive', '10:00:00', '13:00:00', 'Tuesday, Thursday, Saturday', 3),
    (2244, 'KeralaLocalWay', 'Kerala', 'Chennai', 'Sleeper', 1000.00, 150, 120, 'Active', '09:30:00', '14:00:00', 'Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday', 7);


	CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY,
    TrainNumber INT,
    TicketClass VARCHAR(50),
    SeatPreference VARCHAR(50), 
    PassengerName VARCHAR(100),
    BookingDate DATE,
    DateOfTravel DATE,
    TotalAmount DECIMAL(10, 2),
    NumberOfTickets INT,
    TicketPrice DECIMAL(10, 2), -- Include TicketPrice column
    FOREIGN KEY (TrainNumber) REFERENCES Train(TrainNumber)
);


CREATE TABLE Cancellation (
    CancellationID INT PRIMARY KEY IDENTITY,
    BookingID INT, 
    TrainNumber INT, 
    DateOfCancellation DATE,
    NumberOfTickets INT,
    NumberOfTicketsToCancel INT,
    Reason NVARCHAR(255),
    Refund DECIMAL(10, 2),
    FOREIGN KEY (TrainNumber) REFERENCES Train(TrainNumber)
);

-----------------------admin side operations----------------------------------------------
----------------addtrain-------------------
CREATE OR ALTER PROCEDURE AddTrain 
    @TrainNumber INT,
    @TrainName NVARCHAR(100),
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @TicketClass NVARCHAR(50),
    @TicketPrice DECIMAL(10, 2),
    @TotalBerths INT,
    @AvailableBerths INT,
    @Status NVARCHAR(20),
    @DepartureTime TIME,
    @ArrivalTime TIME,
    @DaysOfOperation NVARCHAR(50),
    @Stops INT
AS
BEGIN
    INSERT INTO Train (TrainNumber, TrainName, Source, Destination, TicketClass, TicketPrice, TotalBerths, AvailableBerths, Status, DepartureTime, ArrivalTime, DaysOfOperation, Stops)
    VALUES (@TrainNumber, @TrainName, @Source, @Destination, @TicketClass, @TicketPrice, @TotalBerths, @AvailableBerths, @Status, @DepartureTime, @ArrivalTime, @DaysOfOperation, @Stops);
    PRINT 'Train added successfully Have a Safe Journey !';
END

-------------updateinactive train------------------------------------

CREATE OR ALTER PROCEDURE UpdateInactiveTrain
    @TrainNumber INT,
    @TrainName NVARCHAR(100),
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @TicketClass NVARCHAR(50),
    @TicketPrice DECIMAL(10, 2),
    @TotalBerths INT,
    @AvailableBerths INT,
    @Status NVARCHAR(20),
    @DepartureTime TIME,
    @ArrivalTime TIME,
    @DaysOfOperation NVARCHAR(50),
    @Stops INT
AS
BEGIN
    DECLARE @CurrentStatus NVARCHAR(20);

    -- Check if the train is inactive
    SELECT @CurrentStatus = Status FROM Train
    WHERE TrainNumber = @TrainNumber;

    IF @CurrentStatus = 'Inactive'
    BEGIN
        -- Update the train details
        UPDATE Train
        SET TrainName = @TrainName,
            Source = @Source,
            Destination = @Destination,
            TicketClass = @TicketClass,
            TicketPrice = @TicketPrice,
            TotalBerths = @TotalBerths,
            AvailableBerths = @AvailableBerths,
            Status = @Status,
            DepartureTime = @DepartureTime,
            ArrivalTime = @ArrivalTime,
            DaysOfOperation = @DaysOfOperation,
            Stops = @Stops
        WHERE TrainNumber = @TrainNumber;

        PRINT 'Train details updated successfully!';
    END
    ELSE
    BEGIN
        PRINT 'Cannot update train details. Train is not inactive.';
    END;
END;

---------------------------delete train--------------------------------------

CREATE OR ALTER PROCEDURE DeleteTrain 
    @TrainNumber INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Train WHERE TrainNumber = @TrainNumber)
    BEGIN
        
        UPDATE Train SET Status = 'Inactive' WHERE TrainNumber = @TrainNumber;
        PRINT 'Train deleted successfully!';
    END
    ELSE
    BEGIN
        PRINT 'Train not found.';
    END
END

----view all bookings--------------

CREATE OR ALTER PROCEDURE ViewAllBookings 
AS
BEGIN
    SELECT * FROM Booking;
END



------------------view all cancelations------------------
CREATE OR ALTER  PROCEDURE ViewAllCancellations 
AS
BEGIN
    SELECT * FROM Cancellation;
END





------------------------user side login----------------------
CREATE OR ALTER PROCEDURE BookTicket 
    @TrainNumber INT,
    @TicketClass VARCHAR(50),
    @SeatPreference VARCHAR(50),
    @PassengerName VARCHAR(100),
    @DateOfTravel DATE,
    @NumberOfTickets INT
AS
BEGIN
    DECLARE @TotalAmount DECIMAL(10, 2);
    DECLARE @TicketPrice DECIMAL(10, 2);
    DECLARE @AvailableBerths INT;

    -- Check if the train is active
    IF EXISTS (SELECT 1 FROM Train WHERE TrainNumber = @TrainNumber AND Status = 'Active')
    BEGIN
        -- Check available berths
        SELECT @AvailableBerths = AvailableBerths, @TicketPrice = TicketPrice
        FROM Train 
        WHERE TrainNumber = @TrainNumber AND TicketClass = @TicketClass;

        IF @AvailableBerths >= @NumberOfTickets
        BEGIN
            -- Calculate total amount
            SET @TotalAmount = @TicketPrice * @NumberOfTickets;

            -- Book the ticket
            INSERT INTO Booking (TrainNumber, TicketClass, SeatPreference, PassengerName, BookingDate, DateOfTravel, TotalAmount, NumberOfTickets, TicketPrice)
            VALUES (@TrainNumber, @TicketClass, @SeatPreference, @PassengerName, GETDATE(), @DateOfTravel, @TotalAmount, @NumberOfTickets, @TicketPrice);

            -- Update available berths
            UPDATE Train
            SET AvailableBerths = AvailableBerths - @NumberOfTickets
            WHERE TrainNumber = @TrainNumber;

            -- Display booking details
            SELECT SCOPE_IDENTITY() AS BookingID, @TrainNumber AS TrainNumber, @TicketClass AS TicketClass, @SeatPreference AS SeatPreference, @PassengerName AS PassengerName, GETDATE() AS BookingDate, @DateOfTravel AS DateOfTravel, @TotalAmount AS TotalAmount, @NumberOfTickets AS NumberOfTickets, @TicketPrice AS TicketPrice;

            PRINT 'Ticket booked successfully!';
        END
        ELSE
        BEGIN
            PRINT 'Not enough available berths for booking.';
        END
    END
    ELSE
    BEGIN
        PRINT 'Train is not active. Cannot proceed with booking.';
    END
END;

----print ticket -----------
CREATE OR ALTER PROCEDURE PrintTicket 
    @BookingID INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Booking WHERE BookingID = @BookingID)
    BEGIN
        SELECT 
            B.BookingID,
            B.PassengerName,
            B.BookingDate,
            B.DateOfTravel,
            B.TotalAmount,
            B.NumberOfTickets,
            B.TicketPrice,
            T.TrainNumber,
            T.TrainName,
            T.Source,
            T.Destination,
            T.TicketClass,
            T.DepartureTime,
            T.ArrivalTime,
            T.DaysOfOperation,
            T.Stops,
            B.SeatPreference -- Include seat preference
        FROM Booking B
        INNER JOIN Train T ON B.TrainNumber = T.TrainNumber
        WHERE B.BookingID = @BookingID;
    END
    ELSE
    BEGIN
        PRINT 'Booking not found.';
    END
END;


----------------cancelbookedticket------------------
CREATE OR ALTER PROCEDURE CancelTicket
    @BookingID INT,
    @NumberOfTicketsToCancel INT,  -- New parameter added
    @Reason NVARCHAR(255)  -- New parameter added
AS
BEGIN
    DECLARE @TrainNumber INT;
    DECLARE @NumberOfTickets INT;  -- Added to retrieve the number of tickets from the booking
    DECLARE @TicketPrice DECIMAL(10, 2);
    DECLARE @Refund DECIMAL(10, 2);
    
    -- Retrieve booking details including the number of tickets
    SELECT TOP 1 @TrainNumber = TrainNumber, @NumberOfTickets = NumberOfTickets, @TicketPrice = TicketPrice
    FROM Booking
    WHERE BookingID = @BookingID;

    -- Calculate refund amount
    SET @Refund = @TicketPrice * @NumberOfTicketsToCancel;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Update available berths
        UPDATE Train
        SET AvailableBerths = AvailableBerths + @NumberOfTicketsToCancel
        WHERE TrainNumber = @TrainNumber;

        -- Insert cancellation record
        INSERT INTO Cancellation (BookingID, DateOfCancellation, TrainNumber, NumberOfTickets, NumberOfTicketsToCancel, Reason, Refund)
        VALUES (@BookingID, GETDATE(), @TrainNumber, @NumberOfTickets, @NumberOfTicketsToCancel, @Reason, @Refund);

		  DELETE FROM Booking WHERE BookingID = @BookingID;

        -- Commit transaction
        COMMIT;
    END TRY
    BEGIN CATCH
        -- Rollback transaction on error
        IF @@TRANCOUNT > 0
            ROLLBACK;
        
        -- Handle error
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
        
        -- Return NULL on error
        SELECT NULL AS RefundAmount;
        RETURN;
    END CATCH;

    -- Return refund amount
    SELECT @Refund AS RefundAmount;
END;

select *from Train
select *from Booking 
select *from Cancellation

