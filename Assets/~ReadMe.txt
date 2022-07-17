宏观各个功能位置:

由于是技术展示, 本人没有使用任何插件(只用了一个Odin,给数据分类更整齐,其他无影响).
当然实际工作中还是要选择实用的来缩短开发周期.

数据流动: 1.设计数据.都在_Game/GameDesignSO下. SO即Scriptable Objects. 这也是个人喜欢的开发框架, Scriptable Object是Unity 特色,
我们可以把设计数据很方便的存到一个SO中, 分好Classes就可以. Programmer抓玩法类,设计类的数据都从GameDesignSO抓取并考虑同类的拓展, 尽量让Designer改动
GameDesignSO后游戏流程自动改变,不用知会Programmer, 也不改动已有代码.

2. 游戏进程数据: 看SaveLoadSystem的流程图. 在SaveLoadSystem文件夹下. 
然后是自动保存,快速保存的实现,在上面的基础上确定特定的存档名. 但依然是调用SaveLoadAgent的方法.
实现: 此处用了BinaryFormatter. Json需要写自己的Serialize Dictionary的方法. 或者用插件,Json.Net等.

3. 游戏静态数据是一个Persistent Singleton. 在GameStaticSettings下. 
数据保存方式为PlayerPrefs. 

日期系统:
在Managers/CalendarSystem下. Designer要改时间比例只需要去GameDesignSO改动即可, 不用Programmer配合,数据都是从GameDesignSO里调的. 
Plants下做了简单的树和作物生长. 

地板生成:
在MapGeneration/GroundGeneration下, 此处的算法: 使用PerlinNoise生成一个格子, 再用FloodFill抓取相邻区域,然后分配给不同的Tile, 相关设计数据也在GameDesignSO中,
从此处开始, 地图初始化的流程都是由Event控制的, Awake和Start不适合拿来确认执行顺序, 用Scripts Order Execution设置又很丑陋不容易变通.
没写边缘Rule,因为没找到合适的Tiles. 本人不是画师啊...

植物生成:
在地板初始化数据完成后, 由其Event通知开始生成. 此处简单的设计了不同Terrain的植被比例, 和不同Tile的不同植物.  
分类只做了Plant下的Tree和Crop.  

矿物生成: 
只在RockyLand地表生成. 没做特殊的算法.

人物行为:
在Colonists下, 使用了FSM + Behaviour Tree. 此处添加了两种行为作为演示.
动画实现: 个人作为Programmer对动画的处理方式, 内置的API: CrossFade或者Play,不喜欢用State Machine,动画越多就越乱,  此处人物动物的动画都只有四个,
控制在ActionsManager/AnimationController. 激活动画都调用其, 由其判定具体.
导航系统: 算法使用了A*Pathfinding, 在Managers/PathfindingSystem中, 只做了基本功展示, 市面上Pathfinding已经很多了, 个人平时会使用某个插件.
指令系统: 个人认为Behaviour Tree相比于GOAP更适合建造游戏的玩家AI, 可以让玩家设计优先级. 所以不会主动派出指令,而是由小人FSM中的Think进行判定, 
比如优先级, 比如是否需要.,指令系统在Managers/OrderSystem下,

外观自定义:
只做了Colonist的没有动物的, 太难找合适的动画和Sprites了.
玩家所用的UI和实际的Colonist的外观数据都会根据GameDesignSO的设计去生成,添加新部位只是添加Sprite库,如果3D就添加模型库和Material库,这一类会变化的UI都有代码去生成,
将来添加新的部分, 不用改动UI和代码了. 类似的诸如可选地形等设计.

动物行为:
在Animals下, 使用了最基本的FSM作为展示, 不过个人添加了一个Think作为默认的State, 其他State都会返回这里, 可以减少FSM变成一团面的可能性,
不过个人是不喜欢只用FSM的然后连线硬塞的, 看项目需求吧.






