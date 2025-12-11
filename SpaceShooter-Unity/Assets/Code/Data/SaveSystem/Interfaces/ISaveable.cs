namespace Code
{
    public interface ISaveable
    {
        string SaveKey { get; }      // Уникальный ключ (имя файла / секции)
        System.Type DataType { get; } // Тип DTO для сериализации

        object CaptureState();        // Снять снимок состояния
        void RestoreState(object state); // Восстановить состояние из снапшота
    }
}