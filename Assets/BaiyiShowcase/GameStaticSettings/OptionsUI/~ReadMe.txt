数据位置: OptionsUI. 


数据流向:
1. OptionsUIInitializer(使用GameStaticSettings)
Methods: void InitializeOptionsUI(). 

2. OptionsUI(不是逻辑上的真正的Data,UI Data的步骤已经被Game Engine完成了. 所以对于UI部分的结构, 我们自己的相关Data Class里都是一
些引用, 所以UI部分都不需要SaveLoadSystem.)

3. OptionsUIApplier(使用Data中的Event)
Methods: void Apply***(). 对于UI来说,其Applier的作用就是将Input转化回我们自己的Data. 此处是GameStaticSettings.

