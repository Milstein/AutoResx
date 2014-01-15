AutoResx
========
WebForm项目中，批量对ASPX和ASCX文件生成resx。<br />
<br />
### 主要的工作：<br />
1.清除老的 *.aspx.resx和×.ascx.resx文件；<br />
（如果不清除老文件，新生成文件的meta:resourcekey="****Resource1"后面的序号会自动增加）<br />
2.对每个aspx和ascx文件进行处理<br />
2.1 清除现有的meta:resourcekey="×××Resource1"内容。（原来手工生成的时候，可能会有Resource序号乱的情况）<br />
2.2 切换到Design视图并生成Resource。因为DTE.ExecuteCommand是异步的，<br />
  所以需要显示一个对话框等待视图加载完成后，手工点击关闭对话框之后再调用Tools.GenerateLocalResource命令。<br />
2.3 关闭文档。<br />

<br />
因为在dte中没有找到等待ExecuteCommand完成的方法，所以增加了显示对话框，由人工判断视图加载完成的处理。但是当要处理的文件很多的时候，点这个也是一个很费精力的工作。后来在网上找来一个自动点击按钮的工具Buzof，设置每10秒扫描对话框，并点击确认按钮。这样就基本实现了自动化了。<br />
<br />
### 使用方法：<br />
工程是VS2013的，可以直接使用debug目录下的AutoResx.vsix安装到VS2013的tools目录下。<br />
打开项目后，点击VS的Tools菜单下的AutoResx就可以了。<br />
使用请先备份好你的代码。<br />
