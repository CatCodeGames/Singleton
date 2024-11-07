# 1. Description
This repository contains a base class for creating singletons in Unity, along with auxiliary classes for flexible configuration and management of singletons. 
Supported modes include:
- finding on the scene
- creating a new object
- loading from resources
The singleton is not recreated upon application shutdown or when exiting Play Mode to prevent errors.


# 2. Class Descriptions
### 1. ```MonoSingleton<T>```.
This is the base abstract class for all singleton classes.
- ```static T Instance``` - a property that provides global access to the single instance of the singleton. Automatically creates the instance upon first access.
- ```static bool Instance``` - a property that checks if the singleton instance currently exists.
- ```static event Action<T> InstanceChanged``` - an event that is triggered when the singleton instance is created or destroyed.
- ```virtual void OnInitialize()``` - a method called when the singleton instance is created, used for initialization.
- ```virtual void OnDeinitialize()``` - a method called when the singleton instance is destroyed, used for deinitialization and resource cleanup.

### 2. ```SingletonConfigAttribute```
The ```SingletonConfigAttribute``` is used to configure the creation and management modes of a singleton. It allows for controlling the creation process of singleton instances in Unity with various configuration parameters.
- ```CreationMode``` - A mask controlling the instance creation mode. It can include finding the object on the scene, creating a new instance, or loading from resources.
- ```HideFlags``` - mask that controls object destruction, saving and visibility in inspectors. Moredetails in the [Unity documentation](https://docs.unity3d.com/ScriptReference/HideFlags.html).
- ```DontDestroyOnLoad``` - Indicates whether the object should be preserved when loading a new scene. More details in the [Unity documentation](https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html).
- ```InstanceName``` - The name of the singleton instance in the Unity inspector.
- ```ResourceName``` - The path to the resource, used if ```ObjectCreationMode.LoadFromResources``` is set.


# 3. How to Use
### 1. Create your class by inheriting from the base class ```MonoSingleton<T>```:
- If necessary, override the ```OnInitialize``` and ```OnDeinitialize``` methods, which are called when the Singleton instance is created and destroyed.
### 2. Apply the ```SingletonConfigAttribute``` to configure the creation and management mode:


# 4. Example
### Create Singlton.
``` csharp
    [ObjectCreationConfig(
        creationMode: ObjectCreationMode.CreateNewInstance| ObjectCreationMode.LoadFromResources,
        dontDestroyOnLoad: true,
        hideFlags: HideFlags.None,
        instanceName: nameof(ExampleSingleton),
        resourceName: "Singletons/" + nameof(ExampleSingleton))]
    public sealed class ExampleSingleton : MonoSingleton<ExampleSingleton>
    {        
        public static void DoSomethingStatic()
            => Instance?.DoSomething();

        public void DoSomething()
        {
            ...
        }
    }
```
### Use Singlton
``` csharp
    ExampleSingleton.DoSomethingStatic();
```


# 1. Описание
Этот репозиторий содержит базовый класс для создания синглтонов в Unity, а также вспомогательные классы для гибкой настройки создания и управления синглтонами.
Поддерживается следующие режимы: 
- поиск на сцене
- создание нового объекта
- загрузка из ресурсов
Синглтон не создается повторно при завершении приложения или остановке Play Mode, чтобы избежать ошибок. 


# 2. Описание классов
### 1. ```MonoSingleton<T>```.
  Базовый абстрактный класс для всех классов синглтонов.
- ```static T Instance``` - свойство, предоставляющее глобальный доступ к единственному экземпляру синглтона. При первом обращении автоматически создаёт его.
- ```static bool Instance``` - свойство, проверябщее существует ли в данный момент экземпляр синглтона.  
- ```static event Action<T> InstanceChanged``` - событие, вызываемое при создании или уничтожении экземпляра синглтона.
- ```virtual void OnInitialize()``` - метод, вызываемый при создании экземпляра, служит для инициализации объекта.
- ```virtual void OnDeinitialize()``` - метод, вызываемый при удалении экземпляра. предназначени для деинициализации и освобождения ресурсов, связаннх с объектом.

### 2. ```SingletonConfigAttribute```
  Атрибут ```SingletonConfigAttribute``` используется для настройки режима создания и управления синглтоном.
Позволяет контролировать процесс создания экземпляров синглтона в Unity, предоставляя множество параметров для настройки.
- ```CreationMode``` - маска, управляющая режимом создания экземпляра синглтона. Поиск на сцене, создание нового экземпляра или загрузка из ресурсов.
- ```HideFlags``` - маска управляющая уничтожением, сохранением и видимостью объекта в инспекторе. Подробности в [документации Unity](https://docs.unity3d.com/ScriptReference/HideFlags.html)
- ```DontDestroyOnLoad``` - сохранять ли объект при загрузке новой сцены. Подробности в [документации Unity](https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html)
- ```InstanceName``` - имя экземпляра синглтона в инспекторе Unity.
- ```ResourceName``` - путь к ресурсу, если используется ```ObjectCreationMode.LoadFromResources```.


# 3. Как использовать.
### 1. Создайте свой класс, унаследовав его от базового класса ```MonoSingleton<T>```
- При необходимости переопределите методы ```OnInitialize``` и ```OnDeinitialize```, вызываемые при создании и уничтожении экземпляра синглтон.
### 2. Примените атрибут SingletonConfigAttribute для настройки режима создания и управления.


# 4. Пример
Создание синглтона.
``` csharp
    [ObjectCreationConfig(
        creationMode: ObjectCreationMode.CreateNewInstance| ObjectCreationMode.LoadFromResources,
        dontDestroyOnLoad: true,
        hideFlags: HideFlags.None,
        instanceName: nameof(ExampleSingleton),
        resourceName: "Singletons/" + nameof(ExampleSingleton))]
    public sealed class ExampleSingleton : MonoSingleton<ExampleSingleton>
    {        
        public static void DoSomethingStatic()
            => Instance?.DoSomething();

        public void DoSomething()
        {
            ...
        }
    }
```

Использование синглтона
``` csharp
    ExampleSingleton.DoSomethingStatic();
```
