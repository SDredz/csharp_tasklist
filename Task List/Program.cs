using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task_List
{
    using System;
    using System.Collections.Generic;

    class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }

    class Program
    {
        static List<Task> tasks = new List<Task>();

        static void Main()
        {
            bool exit = false;

            try
            {
                while (!exit)
                {
                    Console.WriteLine("\u001b[1m\u001b[41;1mSySharp Scheduler\u001b[0m\u001b[0m");
                    Console.WriteLine("Press '1' To create a new task");
                    Console.WriteLine("Press '2' To View all tasks");
                    Console.WriteLine("Press '3' To Modify a task");
                    Console.WriteLine("Press '4' To Delete a task");
                    Console.WriteLine("Press '5' To Delete all tasks");
                    Console.WriteLine("Type '0' To Exit");

                    Console.Write("\u001b[1mPlease input a function number: \u001b[0m");
                    string input = Console.ReadLine();

                    Console.WriteLine();

                    if (!int.TryParse(input, out int choice))
                    {
                        Console.WriteLine("\u001b[31mInvalid input. Please enter a valid function number.\u001b[0m");
                        Console.WriteLine();
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            CreateTask();
                            break;
                        case 2:
                            ViewTasks();
                            break;
                        case 3:
                            CheckTaskList4Mod();
                            break;
                        case 4:
                            DeleteTask();
                            break;
                        case 5:
                            DeleteAllTasks();
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("\u001b[31mInvalid choice. Please try again.\u001b[0m");
                            break;
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void CreateTask()
        {
            try
            {
                Console.Write("\u001b[7m'To cancel this operation, type /exit'\u001b[0m\nEnter task name: ");
                string name = Console.ReadLine();

                bool exit = false;

                while (!exit)
                {
                    if (name == "/exit")
                    {
                        exit = true;
                        Console.WriteLine(" ");
                        Main();
                        continue;
                    }
                    else
                    {
                        Console.Write("Enter task description: ");
                        string description = Console.ReadLine();

                        if (description == "/exit")
                        {
                            exit = true;
                            Main();
                            continue;
                        }
                        else
                        {
                            int id = tasks.Count + 1;

                            tasks.Add(new Task { TaskId = id, TaskName = name, TaskDescription = description });

                            Console.WriteLine("\u001b[42;1mTask created successfully!\u001b[42;0m");
                            RepeatCreateTask();
                        }

                        exit = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating a task: {ex.Message}");
            }
        }

        static void RepeatCreateTask()
        {
            try
            {
                Console.WriteLine("\nDo you want to create another task? ");
                string continueLoop = Console.ReadLine();
                bool exit = false;

                if (continueLoop.ToLower() == "yes")
                {
                    Console.WriteLine();
                    CreateTask();
                }
                else if (continueLoop.ToLower() == "no")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInvalid answer, please type 'yes' or 'no'.\u001b[0m");
                    RepeatCreateTask();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while looping back to create task: {ex.Message}");
            }
        }

        static void ViewTasks()
        {
            try
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("\u001b[33mNo tasks found.\u001b[0m");
                }
                else
                {
                    Console.WriteLine("\u001b[36;1mAll Saved Tasks:\u001b[36;0m");

                    foreach (Task task in tasks)
                    {
                        Console.WriteLine($"\u001b[43;1mTask #{task.TaskId}\nName: {task.TaskName}\nDescription: {task.TaskDescription}\u001b[0m\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing tasks: {ex.Message}");
            }
        }

        static void ModifyTask()
        {
            try
            {
                Console.WriteLine("\u001b[7m'To cancel this operation, type 00'\u001b[0m");
                Console.Write("Enter the task ID to modify: ");
                int acTaskId = Convert.ToInt32(Console.ReadLine());

                Task task = tasks.Find(t => t.TaskId == acTaskId);

                bool exit = false;

                while (!exit)
                {
                    if (acTaskId == 0)
                    {
                        exit = true;
                        Console.WriteLine(" ");
                        Main();
                        continue;
                    }
                    else
                    {
                        if (task == null)
                        {
                            Console.WriteLine("Task not found.");
                        }
                        else
                        {
                            Console.Write("Enter the new task Name: ");
                            string newName = Console.ReadLine();
                            task.TaskName = newName;

                            Console.Write("Enter the new task description: ");
                            string newDescription = Console.ReadLine();
                            task.TaskDescription = newDescription;

                            Console.WriteLine("Task modified successfully!");
                            RepeatModifyTask();
                        }

                        exit = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while modifying a task: {ex.Message}");
            }
        }

        static void CheckTaskList4Mod()
        {
            try
            {
                if (tasks.Count == 0)
                {
                    Console.WriteLine("\u001b[33mYou cannot modify an empty task list.\u001b[0m");
                }
                else
                {
                    ModifyTask();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking for task list to modify: {ex.Message}");
            }
        }

        static void RepeatModifyTask()
        {
            try
            {
                Console.WriteLine("\nDo you want to modify another task? ");
                string continueLoop = Console.ReadLine();
                bool exit = false;

                if (continueLoop.ToLower() == "yes")
                {
                    Console.WriteLine();
                    ModifyTask();
                }
                else if (continueLoop.ToLower() == "no")
                {
                    Console.WriteLine("\n\u001b[32mHere are your new tasks:\n \u001b[0m");
                    ViewTasks();
                    exit = true;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInvalid answer, please type 'yes' or 'no'.\u001b[0m");
                    RepeatModifyTask();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while looping back to modify task: {ex.Message}");
            }
        }

        static void DeleteTask()
        {
            try
            {
                Console.WriteLine("\u001b[7m'To cancel this operation, type 00'\u001b[0m");
                Console.Write("Enter the task ID to delete: ");
                int acTaskId = Convert.ToInt32(Console.ReadLine());

                Task task = tasks.Find(t => t.TaskId == acTaskId);

                bool exit = false;

                while (!exit)
                {
                    if (acTaskId == 0)
                    {
                        exit = true;
                        Console.WriteLine(" ");
                        Main();
                        continue;
                    }
                    else
                    {
                        if (task == null)
                        {
                            Console.WriteLine("Task not found.");
                        }
                        else
                        {
                            tasks.Remove(task);
                            Console.WriteLine("\u001b[41;1mTask deleted successfully!\u001b[41;0m");
                            RepeatDeleteTask();
                        }

                        exit = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting a task: {ex.Message}");
            }
        }

        static void RepeatDeleteTask()
        {
            try
            {
                Console.WriteLine("\nDo you want to delete another task? ");
                string continueLoop = Console.ReadLine();
                bool exit = false;

                if (continueLoop.ToLower() == "yes")
                {
                    Console.WriteLine();
                    DeleteTask();
                }
                else if (continueLoop.ToLower() == "no")
                {
                    Console.Write("");
                    ViewTasks();
                    exit = true;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInvalid answer, please type 'yes' or 'no'.\u001b[0m");
                    RepeatDeleteTask();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while looping back to delete task: {ex.Message}");
            }
        }

        static void DeleteAllTasks()
        {
            try
            {
                tasks.Clear();
                Console.WriteLine("\u001b[41;1mAll tasks deleted successfully!\u001b[41;0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting all tasks: {ex.Message}");
            }
        }
    }



}
