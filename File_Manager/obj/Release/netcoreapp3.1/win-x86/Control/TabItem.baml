   M S B A M L   `   `   `  ???? G  CFile_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:File_Manager.ControlFile_Manager.Control  +clr-namespace:File_ManagerFile_Manager  ?$clr-namespace:File_Manager.ViewModelFile_Manager.ViewModel  "    File_Manager.Control.TabItem   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:File_Manager.Control     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7behav+http://schemas.microsoft.com/xaml/behaviors  a ]GongSolutions.WPF.DragDrop, Version=3.0.0.0, Culture=neutral, PublicKeyToken=91f1945125b7a587ddurn:gong-wpf-dragdrop  #uiclr-namespace:File_Manager  4	viewmodel$clr-namespace:File_Manager.ViewModel    ? 	Resources  1  +File_Manager.Control.ConverterObjectToImage   ImgConverter??%   &	          .???  ? RowDefinitions ?? $??* ? ? ColumnDefinitions ?? $	??300 ??? $??* ?.+??? $??0??
???????
???   ?$??1 0 1 1q?._??? 
???????$??10q?$	??300?????? *SelectObjInfo+???? .R??? 
???   ?$??0 0 0 1q?._??? $
??Wrap=? ?? Padding$ 5q?$??10q?$??20(?$	??250??$??DemiBold"????? ?? -   $??listMain??$??1??
???????$??Center=?$??Center=?$??Disabled=?)  #GongSolutions.Wpf.DragDrop.DragDrop   IsDragSource$
 True??   IsDropTarget$
 True??   File_Manager.DragDropCl   instance   DropHandler# Z    UseDefaultDragAdorner$
 True??#	   UseDefaultEffectDataTemplate$
	 True??
 ? DataContext
 ?? *.+???? *
CurrentDir+ ?? SelectedItem ?? *
SelectedOb+ ? ContextMenu ?? .@?m? $??Відкрити?? m? Command ?? *GoINCommand+ m? CommandParameter ?? *
SelectedOb+m? $2??,Відкрити нову вкладинку?? ?? *parent.AddNewTabItemCommand+m? $??Оновити?? ?? *ReloadCommand+m? $0??*Повернутися на головну?? ?? *GoHomeCommand+m? $??Вставити?? ?? *parent.PasteCommand+m? $0??*Створити новий каталог?? ?? *parent.AddFolderCommand+m? $??Фільтр??.@?m? -    m?Name$ docxx??$??Файли .docx??
???   ?$ Файли .docx?? ?? *FillterCollectionCommand+m? -   $	 pyy??$??Файли .py??
???   ?$ Файли .py?? ?? *FillterCollectionCommand+m? -   $	 css??$??Файли .cs??
???   ?$ Файли .cs?? ?? *FillterCollectionCommand+?? 
???   ?$??0 1 0 0q?._?m? -   $ uuser??$"??Користувацький??
???   ?$" Користувацький?? ?? *FillterCollectionCommand+m? $??Сортування??.@?m? -   $
 time??$??За часом??
???   ?$ За часом?? ?? *SortCollectionCommand+m? -   $
 type??$??За типом??
???   ?$ За типом?? ?? *SortCollectionCommand+m? -   $
 name??$??За ім'ям??
???   ?$ За ім'ям?? ?? *SortCollectionCommand+m? $??Обраний файл??{??? *
IsSelected+.@?m? $??Копіювати?? ?? *parent.CopyCommand+m? $??Вирізати?? ?? *parent.CutCommand+m? $??Вставити?? ?? *parent.PasteCommand+m? $??Видалити?? ?? *parent.DeleteCommand+m? $??Змінити ім'я?? ?? *RenameCommand+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ MouseDoubleClick??*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  -	   GoInCom??   Command ?? *GoINCommand+   CommandParameter ??  ?? ElementName$ listMain?? ?? Path$ SelectedItem????? .R?? ????  ?? 
TargetType" ??.???  ?? Property ?@J?SnapsToDevicePixels ?? Value$
 True????  ? ??$ 4q???  ? ?? ?? *HorizontalContentAlignment+ ?? RelativeSource ??  ?? AncestorType" ????  ? ?? ?? *VerticalContentAlignment+ ?? " ????  ? ??
 ???????  ? ??
 ???????  ? ??$ 1q???  ? ??$	 150????  ? ??$	 150????  ? ?? ??  ?? 
TargetType" ??.R?? .+???  ??Name$ Bd?? ?? CornerRadius$ 10??#??z??#??z??#??z?? ?? Padding# z??  J? SnapsToDevicePixels$
  true??$??10q?._??? $??1??#??z??#  z  #??z??! ?? Triggers! ]? " ]? 
Conditions" ?? # ?? Property# ?@J?IsMouseOver$ ?? Value$
$ True??./??? % ?? 
TargetName$% Bd?? ? ??
 ?????]? " ?? # ?@??IsSelectionActive$$ False???? # ?@??
IsSelected$
$ True??./??? $% Bd?? ? ??
 ?????]? " ?? # ?@??IsSelectionActive$
$ True???? # ?@??
IsSelected$
$ True??./??? $% Bd?? ? ??
 ?????P? & P? Property& ? {?' P? Value$' False??. ??? $% Bd??# ? ?? ? ?????? .R?? 
??????  ?? ?? $
??Auto ?.+??? ???? *.+( ?? 	Converter#( [  ?? 
???   ?$??1??$??15(?$??CharacterEllipsis=?$??Center=????? *NAME+