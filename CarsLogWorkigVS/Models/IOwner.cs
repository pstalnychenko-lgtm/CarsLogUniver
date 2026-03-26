using System;

namespace CarsLogWorkig.Models
{
    // Інтерфейс для власника: управління своїми автомобілями, водіями, витратами
    public interface IOwner : IUser
    {
        //Особисті дані 
        string FirstNameByOwner { get; }       // Переглянути ім'я
        string LastNameByOwner { get; }        // Переглянути прізвище
        string PhoneByOwner { get; }           // Переглянути номер телефону
        string AddressByOwner { get; }         // Переглянути адресу
        DateTime DateOfPurchaseTheCar { get; } // Переглянути дату покупки автомобіля
        string DateOfPurchaseTheCarFormatted { get; } // Відформатована дата покупки

        // Дії власника
        void AddDriverToVehicle(Vehicle vehicle, Driver driver); // Додати водія до автомобіля
        void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver); // Видалити водія з автомобіля

        void AddNoteToVehicle(Vehicle vehicle, Note note);         // Додати нотатку до автомобіля
        void AddExpenseToVehicle(Vehicle vehicle, Expense expense); // Додати витрату до автомобіля

        void AddDocumentToVehicle(Vehicle vehicle, Document document);  // Додати документ до автомобіля
        void AddServiceRecord(Vehicle vehicle, ServiceRecord record);    // Додати запис про сервіс

        bool IsVehicleOwner(Vehicle vehicle); // Перевірити, чи є власником конкретного авто
    }
}
