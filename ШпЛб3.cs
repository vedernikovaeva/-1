using System;
using System.Collections.Generic;

//1. Необхідно написати систему керуванням дроном.
//Припустимо, що дрон матеріальна точка яка вміє рухатись
//в 6 напрямах: вгору (U), вниз (D), вперед (F), назад (B),
//вліво (L) та вправо (R) . Нехай дрон приймає програму переміщення
//як послідовність команд у форматі <напрям руху> <кількість метрів>,
//наприклад рух вгору на 1 метр, а потім вперед на 3 метри і посадка
//вниз на 1 метр запишется як: U 1 F 3 D 1.  Визначити координати дрона
//після виконання такої програми. Передбачити можливість додавання нових команд,
//наприклад скидування вантажу.

public interface ICommand
{
    void Execute(Drone drone);
}

public class Drone
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }

    public void MoveUp(int distance)
    {
        Z += distance;
        Console.WriteLine($"Вгору на {distance} метрів.Координати зараз: X ={X}, Y ={Y}, Z ={Z} ");
    }

    public void MoveDown(int distance)
    {
        Z -= distance;
        Console.WriteLine($"Вниз на {distance} метрів. Координати зараз: X ={X}, Y ={Y}, Z ={Z}");
    }

    public void MoveForward(int distance)
    {
        Y += distance;
        Console.WriteLine($"Вперед на {distance} метрів.Координати зараз: X ={X}, Y ={Y}, Z ={Z}");
    }

    public void MoveBackward(int distance)
    {
        Y -= distance;
        Console.WriteLine($"Назад на {distance} метрів. Координати зараз: X ={X}, Y ={Y}, Z ={Z}");
    }

    public void MoveLeft(int distance)
    {
        X -= distance;
        Console.WriteLine($"Вліво на {distance} метрів. Координати зараз: X ={X}, Y ={Y}, Z ={Z}");
    }

    public void MoveRight(int distance)
    {
        X += distance;
        Console.WriteLine($"Вправо {distance} метрів. Координати зараз: X ={X}, Y ={Y}, Z ={Z}");
    }
}

public class MoveUpCommand : ICommand
{
    private int _distance;

    public MoveUpCommand(int distance)
    {
        _distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.MoveUp(_distance);
    }
}

public class MoveDownCommand : ICommand
{
    private int _distance;

    public MoveDownCommand(int distance)
    {
        _distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.MoveDown(_distance);
    }
}

public class MoveForwardCommand : ICommand
{
    private int _distance;

    public MoveForwardCommand(int distance)
    {
        _distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.MoveForward(_distance);
    }
}

public class MoveBackwardCommand : ICommand
{
    private int _distance;

    public MoveBackwardCommand(int distance)
    {
        _distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.MoveBackward(_distance);
    }
}

public class MoveLeftCommand : ICommand
{
    private int _distance;

    public MoveLeftCommand(int distance)
    {
        _distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.MoveLeft(_distance);
    }
}

public class MoveRightCommand : ICommand
{
    private int _distance;

    public MoveRightCommand(int distance)
    {
        _distance = distance;
    }

    public void Execute(Drone drone)
    {
        drone.MoveRight(_distance);
    }
}

public class DropCargoCommand : ICommand
{
    public void Execute(Drone drone)
    {
        Console.WriteLine("Вантаж скинутий");
    }
}

public class DroneProgram
{
    private List<ICommand> _commands = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void ExecuteProgram(Drone drone)
    {
        foreach (var command in _commands)
        {
            command.Execute(drone);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Drone drone = new Drone();

        DroneProgram program = new DroneProgram();
        program.AddCommand(new MoveUpCommand(1));
        program.AddCommand(new MoveForwardCommand(3));
        program.AddCommand(new MoveDownCommand(1));
        program.AddCommand(new DropCargoCommand());
        program.ExecuteProgram(drone);

        Console.WriteLine($"Кінцеві координати: X ={drone.X}, Y ={drone.Y}, Z ={drone.Z}");
    }
}