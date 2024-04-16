using System;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace TrainReservationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            // Get the width of the console window
            int windowWidth = Console.WindowWidth;

            // Set the initial left position for the text
            int left = 0;

            // Continuously update the position of the text until it reaches the right edge of the console
            while (left <= windowWidth)
            {
                Console.Clear(); // Clear the console to update the text position

                // Set the cursor position
                Console.SetCursorPosition(left, Console.CursorTop);

                // Write the text
                Console.WriteLine("Welcome to TRAIN RESERVATION SYSTEM");

                // Reset console text color
                Console.ResetColor();

                // Pause for a short duration to control the speed of movement
                Thread.Sleep(50);

                // Check if Enter key is pressed
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    // Break out of the loop if Enter key is pressed
                    break;
                }

                // Increment the left position
                left++;
            }

            // Keep the console open until a key is pressed
            Console.ReadKey();

            string connectionString = "Server=ICS-LT-9S4R9K3;Database=MyNewPrjDB;Integrated Security=True;";

            // Admin login details
            string adminUsername = "admin";
            string adminPassword = "admin@123";

            // User login details
            string userLoginId = "user";
            string userPassword = "user@123";

            while (true)
            {
                Console.WriteLine("Select Option:");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. User Login");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AdminLogin(adminUsername, adminPassword, connectionString);
                        break;
                    case "2":
                        UserLogin(userLoginId, userPassword, connectionString);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.ReadKey();
            }
        }

        static void AdminLogin(string adminUsername, string adminPassword, string connectionString)
        {
            Console.WriteLine("\nAdmin Login:");
            Console.Write("Username: ");
            string usernameInput = Console.ReadLine();
            Console.Write("Password: ");
            string passwordInput = Console.ReadLine();

            if (usernameInput == adminUsername && passwordInput == adminPassword)
            {
                Console.WriteLine("Admin logged in successfully.");

                while (true)
                {
                    Console.WriteLine("\nAdmin Operations Menu:");
                    Console.WriteLine("1. Add Train");
                    Console.WriteLine("2. Update Train");
                    Console.WriteLine("3. Delete Train");
                    Console.WriteLine("4. View All Bookings");
                    Console.WriteLine("5. View All Cancellations");
                    Console.WriteLine("6. Logout");
                    Console.Write("Enter your choice: ");
                    string adminChoice = Console.ReadLine();

                    switch (adminChoice)
                    {
                        case "1":
                            AddTrain(connectionString);
                            break;
                        case "2":
                            UpdateTrain(connectionString);
                            break;
                        case "3":
                            DeleteTrain(connectionString);
                            break;
                        case "4":
                            ViewAllBookings(connectionString);
                            break;
                        case "5":
                            ViewAllCancellations(connectionString);
                            break;
                        case "6":
                            Console.WriteLine("Logging out...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid admin login credentials.");
            }
        }

        static void UserLogin(string userLoginId, string userPassword, string connectionString)
        {
            Console.WriteLine("\nUser Login:");
            Console.Write("Login ID: ");
            string loginIdInput = Console.ReadLine();
            Console.Write("Password: ");
            string passwordInput = Console.ReadLine();

            if (loginIdInput == userLoginId && passwordInput == userPassword)
            {
                Console.WriteLine("User logged in successfully.");

                while (true)
                {
                    Console.WriteLine("\nUser Operations Menu:");
                    Console.WriteLine("1. Book Ticket");
                    Console.WriteLine("2. Cancel Ticket");
                    Console.WriteLine("3. Print Ticket");
                    Console.Write("Enter your choice: ");
                    string userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            BookTicket(connectionString);
                            break;
                        case "2":
                            CancelTicket(connectionString);
                            break;
                        case "3":
                            PrintTicket(connectionString);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid user login credentials.");
            }
        }
        static void AddTrain(string connectionString)
        {
            // Prompt the user for train details
            Console.WriteLine("Enter Train Number:");
            int trainNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Train Name:");
            string trainName = Console.ReadLine();
            Console.WriteLine("Enter Source:");
            string source = Console.ReadLine();
            Console.WriteLine("Enter Destination:");
            string destination = Console.ReadLine();
            Console.WriteLine("Enter Ticket Class:");
            string ticketClass = Console.ReadLine();
            Console.WriteLine("Enter Ticket Price:");
            decimal ticketPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Total Berths:");
            int totalBerths = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Available Berths:");
            int availableBerths = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Status:");
            string status = Console.ReadLine();
            Console.WriteLine("Enter Departure Time (HH:MM:SS):");
            TimeSpan departureTime = TimeSpan.Parse(Console.ReadLine());
            Console.WriteLine("Enter Arrival Time (HH:MM:SS):");
            TimeSpan arrivalTime = TimeSpan.Parse(Console.ReadLine());
            Console.WriteLine("Enter Days of Operation:");
            string daysOfOperation = Console.ReadLine();
            Console.WriteLine("Enter Stops:");
            int stops = Convert.ToInt32(Console.ReadLine());

            try
            {
                // Call the stored procedure to add a train
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddTrain", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@TrainNumber", trainNumber);
                    command.Parameters.AddWithValue("@TrainName", trainName);
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@TicketClass", ticketClass);
                    command.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                    command.Parameters.AddWithValue("@TotalBerths", totalBerths);
                    command.Parameters.AddWithValue("@AvailableBerths", availableBerths);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@DepartureTime", departureTime);
                    command.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
                    command.Parameters.AddWithValue("@DaysOfOperation", daysOfOperation);
                    command.Parameters.AddWithValue("@Stops", stops);

                    // Execute the command
                    command.ExecuteNonQuery();

                    Console.WriteLine("Train added successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding train: " + ex.Message);
            }
        }


        static void UpdateTrain(string connectionString)
        {
            try
            {
                // Get input for train number
                Console.WriteLine("Enter Train Number:");
                int trainNumber = Convert.ToInt32(Console.ReadLine());

                // Connect to the database and check train status
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand statusCommand = new SqlCommand("SELECT Status FROM Train WHERE TrainNumber = @TrainNumber", connection);
                    statusCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                    string status = (string)statusCommand.ExecuteScalar();

                    if (status == "Inactive")
                    {
                        // Train is inactive, proceed with updating
                        Console.WriteLine("Train is inactive. Proceeding with update...");

                        // Get input for other train details
                        Console.WriteLine("Enter Train Name:");
                        string trainName = Console.ReadLine();
                        Console.WriteLine("Enter Source:");
                        string source = Console.ReadLine();
                        Console.WriteLine("Enter Destination:");
                        string destination = Console.ReadLine();
                        Console.WriteLine("Enter Ticket Class:");
                        string ticketClass = Console.ReadLine();
                        Console.WriteLine("Enter Ticket Price:");
                        decimal ticketPrice = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Enter Total Berths:");
                        int totalBerths = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Available Berths:");
                        int availableBerths = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Status:");
                        string newStatus = Console.ReadLine();
                        Console.WriteLine("Enter Departure Time (HH:MM:SS):");
                        TimeSpan departureTime = TimeSpan.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Arrival Time (HH:MM:SS):");
                        TimeSpan arrivalTime = TimeSpan.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Days of Operation:");
                        string daysOfOperation = Console.ReadLine();
                        Console.WriteLine("Enter Stops:");
                        int stops = Convert.ToInt32(Console.ReadLine());

                        // Call the stored procedure to update train
                        SqlCommand command = new SqlCommand("UpdateInactiveTrain", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainNumber", trainNumber);
                        command.Parameters.AddWithValue("@TrainName", trainName);
                        command.Parameters.AddWithValue("@Source", source);
                        command.Parameters.AddWithValue("@Destination", destination);
                        command.Parameters.AddWithValue("@TicketClass", ticketClass);
                        command.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                        command.Parameters.AddWithValue("@TotalBerths", totalBerths);
                        command.Parameters.AddWithValue("@AvailableBerths", availableBerths);
                        command.Parameters.AddWithValue("@Status", newStatus);
                        command.Parameters.AddWithValue("@DepartureTime", departureTime);
                        command.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
                        command.Parameters.AddWithValue("@DaysOfOperation", daysOfOperation);
                        command.Parameters.AddWithValue("@Stops", stops);
                        command.ExecuteNonQuery();

                        Console.WriteLine("Train details updated successfully!");
                    }
                    else
                    {
                        // Train is not inactive, cannot proceed with update
                        Console.WriteLine("Cannot update train details. Train is not inactive.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating train: " + ex.Message);
            }
        }


        static void DeleteTrain(string connectionString)
        {
            try
            {
                // Get input for train number
                Console.WriteLine("Enter Train Number:");
                int trainNumber = Convert.ToInt32(Console.ReadLine());

                // Connect to the database and call the stored procedure
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteTrain", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TrainNumber", trainNumber);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Train deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Train not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting train: " + ex.Message);
            }
        }

        static void ViewAllBookings(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create command to select all bookings
                    SqlCommand command = new SqlCommand("SELECT * FROM Booking", connection);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if there are any rows returned
                    if (reader.HasRows)
                    {
                        Console.WriteLine("All Bookings:");
                        Console.WriteLine("==============");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Booking ID: {reader["BookingID"]}, Train Number: {reader["TrainNumber"]}, Passenger Name: {reader["PassengerName"]}, Date of Travel: {reader["DateOfTravel"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No bookings found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing bookings: " + ex.Message);
            }
        }

        static void ViewAllCancellations(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create command to select all cancellations
                    SqlCommand command = new SqlCommand("SELECT * FROM Cancellation", connection);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if there are any rows returned
                    if (reader.HasRows)
                    {
                        Console.WriteLine("All Cancellations:");
                        Console.WriteLine("==================");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Cancellation ID: {reader["CancellationID"]}, Booking ID: {reader["BookingID"]}, Train Number: {reader["TrainNumber"]}, Date of Cancellation: {reader["DateOfCancellation"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No cancellations found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing cancellations: " + ex.Message);
            }
        }



        //----------------------------------user side------------------------------------------//
        static void DisplayAvailableTrains(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    SqlCommand command = new SqlCommand("SELECT * FROM Train", connection);

                    // Execute the command
                    SqlDataReader reader = command.ExecuteReader();

                    // Check if there are any available trains
                    if (reader.HasRows)
                    {
                        // Print the header
                        Console.WriteLine("Available Trains:");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("|Train Number| Train Name | Source  | Destination| Ticket Class | Departure Time | Arrival Time | Days of Operation| Stops|");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

                        // Print each available train
                        while (reader.Read())
                        {
                            Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-10} {5,-8} {6,-8} {7,-15} {8,-5}", reader["TrainNumber"], reader["TrainName"], reader["Source"], reader["Destination"], reader["TicketClass"], reader["DepartureTime"], reader["ArrivalTime"], reader["DaysOfOperation"], reader["Stops"]);


                        }

                        // Print the footer
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("No available trains found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying available trains: " + ex.Message);
            }
        }

        static void BookTicket(string connectionString)
        {
            DisplayAvailableTrains(connectionString);
            try
            {
                Console.WriteLine("Enter Train Number:");
                int trainNumber = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the train exists and is active
                    SqlCommand checkTrainStatusCommand = new SqlCommand("SELECT Status, AvailableBerths FROM Train WHERE TrainNumber = @TrainNumber", connection);
                    checkTrainStatusCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                    SqlDataReader reader = checkTrainStatusCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string trainStatus = (string)reader["Status"];
                        int availableBerths = (int)reader["AvailableBerths"];

                        reader.Close();

                        if (trainStatus == "Active")
                        {
                            Console.WriteLine("Enter Ticket Class:");
                            string ticketClass = Console.ReadLine();

                            // Check available berths for the specified class
                            SqlCommand checkAvailableBerthsCommand = new SqlCommand("SELECT TicketPrice FROM Train WHERE TrainNumber = @TrainNumber AND TicketClass = @TicketClass", connection);
                            checkAvailableBerthsCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                            checkAvailableBerthsCommand.Parameters.AddWithValue("@TicketClass", ticketClass);
                            SqlDataReader classReader = checkAvailableBerthsCommand.ExecuteReader();

                            if (classReader.Read())
                            {
                                decimal ticketPrice = (decimal)classReader["TicketPrice"];

                                classReader.Close(); // Close the second SqlDataReader here

                                Console.WriteLine("Enter Seat Preference:");
                                string seatPreference = Console.ReadLine();

                                Console.WriteLine("Enter Passenger Name:");
                                string passengerName = Console.ReadLine();

                                Console.WriteLine("Enter Date of Travel (yyyy-mm-dd):");
                                DateTime dateOfTravel = Convert.ToDateTime(Console.ReadLine());

                                Console.WriteLine("Enter Number of Tickets:");
                                int numberOfTickets = Convert.ToInt32(Console.ReadLine());

                                if (availableBerths >= numberOfTickets)
                                {
                                    // Calculate total amount
                                    decimal totalAmount = ticketPrice * numberOfTickets;

                                    // Book the ticket
                                    SqlCommand bookTicketCommand = new SqlCommand("INSERT INTO Booking (TrainNumber, TicketClass, SeatPreference, PassengerName, BookingDate, DateOfTravel, TotalAmount, NumberOfTickets, TicketPrice) VALUES (@TrainNumber, @TicketClass, @SeatPreference, @PassengerName, GETDATE(), @DateOfTravel, @TotalAmount, @NumberOfTickets, @TicketPrice)", connection);
                                    bookTicketCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                                    bookTicketCommand.Parameters.AddWithValue("@TicketClass", ticketClass);
                                    bookTicketCommand.Parameters.AddWithValue("@SeatPreference", seatPreference);
                                    bookTicketCommand.Parameters.AddWithValue("@PassengerName", passengerName);
                                    bookTicketCommand.Parameters.AddWithValue("@DateOfTravel", dateOfTravel);
                                    bookTicketCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                                    bookTicketCommand.Parameters.AddWithValue("@NumberOfTickets", numberOfTickets);
                                    bookTicketCommand.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                                    bookTicketCommand.ExecuteNonQuery();

                                    // Update available berths
                                    SqlCommand updateBerthsCommand = new SqlCommand("UPDATE Train SET AvailableBerths = AvailableBerths - @NumberOfTickets WHERE TrainNumber = @TrainNumber", connection);
                                    updateBerthsCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                                    updateBerthsCommand.Parameters.AddWithValue("@NumberOfTickets", numberOfTickets);
                                    updateBerthsCommand.ExecuteNonQuery();

                                    Console.WriteLine("Ticket booked successfully!");
                                    Console.WriteLine($"Booking Date: {DateTime.Now}");
                                    Console.WriteLine($"Ticket Price: {ticketPrice:C}");
                                    Console.WriteLine($"Total Amount: {totalAmount:C}");
                                }
                                else
                                {
                                    Console.WriteLine("Not enough available berths for booking.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ticket class not found for the specified train.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Train is not active. Cannot proceed with booking.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Train not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error booking ticket: " + ex.Message);
            }
        }

        static void PrintTicket(string connectionString)
        {
            try
            {
                Console.WriteLine("Enter the Booking ID:");
                int bookingID = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    SqlCommand command = new SqlCommand("PrintTicket", connection);
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@BookingID", bookingID);


                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Booking ID: {reader["BookingID"]}");
                            Console.WriteLine($"Passenger Name: {reader["PassengerName"]}");
                            Console.WriteLine($"Booking Date: {reader["BookingDate"]}");
                            Console.WriteLine($"Date of Travel: {reader["DateOfTravel"]}");
                            Console.WriteLine($"Total Amount: {reader["TotalAmount"]}");
                            Console.WriteLine($"Number of Tickets: {reader["NumberOfTickets"]}");
                            Console.WriteLine($"Ticket Price: {reader["TicketPrice"]}");
                            Console.WriteLine($"Train Number: {reader["TrainNumber"]}");
                            Console.WriteLine($"Train Name: {reader["TrainName"]}");
                            Console.WriteLine($"Source: {reader["Source"]}");
                            Console.WriteLine($"Destination: {reader["Destination"]}");
                            Console.WriteLine($"Ticket Class: {reader["TicketClass"]}");
                            Console.WriteLine($"Departure Time: {reader["DepartureTime"]}");
                            Console.WriteLine($"Arrival Time: {reader["ArrivalTime"]}");
                            Console.WriteLine($"Days of Operation: {reader["DaysOfOperation"]}");
                            Console.WriteLine($"Stops: {reader["Stops"]}");
                            Console.WriteLine($"Seat Preference: {reader["SeatPreference"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Booking not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error printing ticket: " + ex.Message);
            }
        }
        static void CancelTicket(string connectionString)
        {
            try
            {
                Console.WriteLine("Enter Booking ID:");
                int bookingID = Convert.ToInt32(Console.ReadLine());

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve booking details
                    SqlCommand retrieveBookingCommand = new SqlCommand("SELECT TOP 1 TrainNumber, NumberOfTickets, TicketPrice FROM Booking WHERE BookingID = @BookingID", connection);
                    retrieveBookingCommand.Parameters.AddWithValue("@BookingID", bookingID);
                    SqlDataReader reader = retrieveBookingCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        int trainNumber = (int)reader["TrainNumber"];
                        int NumberOfTickets = (int)reader["NumberOfTickets"];
                        decimal ticketPrice = (decimal)reader["TicketPrice"];
                        reader.Close();

                        Console.WriteLine($"Enter number of tickets to cancel (Maximum {NumberOfTickets}):");
                        int numberOfTicketsToCancel = Convert.ToInt32(Console.ReadLine());

                        if (numberOfTicketsToCancel <= NumberOfTickets)
                        {
                            // Calculate refund amount
                            decimal refund = ticketPrice * numberOfTicketsToCancel;

                            Console.WriteLine("Enter reason for cancellation:");
                            string cancellationReason = Console.ReadLine();

                            // Begin transaction for cancellation
                            SqlTransaction transaction = connection.BeginTransaction();

                            try
                            {
                                // Update available berths
                                SqlCommand updateBerthsCommand = new SqlCommand("UPDATE Train SET AvailableBerths = AvailableBerths + @NumberOfTickets WHERE TrainNumber = @TrainNumber", connection, transaction);
                                updateBerthsCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                                updateBerthsCommand.Parameters.AddWithValue("@NumberOfTickets", numberOfTicketsToCancel);
                                updateBerthsCommand.ExecuteNonQuery();

                                // Insert cancellation record
                                SqlCommand insertCancellationCommand = new SqlCommand("INSERT INTO Cancellation (BookingID, DateOfCancellation, TrainNumber, NumberOfTickets, NumberOfTicketsToCancel, Reason, Refund) VALUES (@BookingID, GETDATE(), @TrainNumber, @NumberOfTickets, @NumberOfTicketsToCancel, @Reason, @Refund)", connection, transaction);
                                insertCancellationCommand.Parameters.AddWithValue("@BookingID", bookingID);
                                insertCancellationCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                                insertCancellationCommand.Parameters.AddWithValue("@NumberOfTickets", NumberOfTickets);
                                insertCancellationCommand.Parameters.AddWithValue("@NumberOfTicketsToCancel", numberOfTicketsToCancel);
                                insertCancellationCommand.Parameters.AddWithValue("@Reason", cancellationReason);
                                insertCancellationCommand.Parameters.AddWithValue("@Refund", refund);
                                insertCancellationCommand.ExecuteNonQuery();



                                SqlCommand deleteBookingCommand = new SqlCommand("DELETE FROM Booking WHERE BookingID = @BookingID", connection, transaction);
                                deleteBookingCommand.Parameters.AddWithValue("@BookingID", bookingID);
                                deleteBookingCommand.ExecuteNonQuery();



                                // Commit transaction
                                transaction.Commit();

                                Console.WriteLine($"Ticket cancelled successfully. Refund amount: {refund:C}");
                            }
                            catch (Exception ex)
                            {
                                // Rollback transaction on error
                                transaction.Rollback();
                                Console.WriteLine("Error cancelling ticket: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of tickets to cancel. Maximum available: " + NumberOfTickets);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Booking not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cancelling ticket: " + ex.Message);
            }
        }

    }
}



