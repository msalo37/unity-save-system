## Unity save system
Save system for unity which i use in my projects. It stores all saves in the dictionary, and saves it in one file, the name and path of which can be set.

You can check the operation of the saving system using the example scene.
### How to use

First you need to create scriptable object **SaveSettings** in Resources folder

Create Object and assing script **SaveController**.

Then you need to add **IDataHolder** and **IDataLoader** components to object. You can add **JSONDataLoader** or **BinaryDataLoader** and **DictionaryDataHolder** or you can write your own script ;)

To set your class to saves you need:
```c#
SaveController.SetSave("someKey", myClass);
```

To load your class you need:
```c#
myClass = SaveController.GetSave<MyClass>("someKey");
```
```c#
if (SaveController.TryToGetSave("someKey", out MyClass data))
    myClass = data;
```
```c#
myClass = SaveController.TryToGetSave("someKey", out MyClass data) ? data : new MyClass();
```

#### Save controller have events *onSaving* and *onSavesLoaded*

To save your game you just need type:
```c#
SaveController.SaveAll();
```

If you have errors when you trying access to SaveController you need to change script execution order and set SaveController to call earlier