using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponents
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public ComponentType ComponentType { get; set; }// Тип компонента (наприклад, масло, фільтр, шини тощо)

        public string CustomPartName { get; set; }// Назва компонента, якщо вибрано "Other" 

        public bool IsReplaced { get; set; } // Чи був компонент замінений

        public bool IsBeInNormalCondition { get; set; } // Чи знаходиться компонент в нормальному стані

        public string Notes { get; set; }
    }

    public enum ComponentType
    {
        OilFilter,// Масляний фільтр
        AirFilter,// Повітряний фільтр
        FuelFilter,// Паливний фільтр
        Tires,// Шини
        Brakes,// Гальма
        TimingBelt,// Ремінь ГРМ
        Battery,// Акумулятор
        SparkPlugs,// Свічки запалювання
        CoolantFluid,// Охолоджуюча рідина
        BrakeFluid,// Гальмівна рідина
        Other// Інший компонент
    }
}
