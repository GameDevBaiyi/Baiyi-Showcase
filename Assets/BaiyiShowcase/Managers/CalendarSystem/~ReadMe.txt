数据位置: Calendar

数据流向:
1. CalendarInitializer:DataAllocator;
Method: 1.void Initialize(); 2. void RestoreData.....();

2. Calendar
只含数据和对应Event,年月日时分等等.

3. CalendarApplier
Subscribe所有Events.

4. CalendarDataCollector:DataCollector


其他功能组件:
CalendarUpdater
含有对Calendar和GameDesignSO的引用,在Update中更新分. 由于CalendarApplier挂钩Events. 会实现所有时间的更新.