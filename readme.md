# Apache ZooKeeper .NET async Client

<p>
<img src="https://svn.apache.org/repos/asf/comdev/project-logos/originals/zookeeper.svg" width="400">
</p>

![NuGet](https://img.shields.io/github/release/shayhatsor/zookeeper.svg?style=flat&label=Latest%20Release)](https://github.com/shayhatsor/zookeeper/releases/latest)
* Supports .NET 4.61 and above, including .NET Core.
* Fully Task-based Asynchronous (async/await).
* Follows the logic of the official Java client to the letter, in fact the code is almost identical. 
* NuGets
  * Client [ZooKeeperNetEx](https://www.nuget.org/packages/ZooKeeperNetEx)
  * Recipes [ZooKeeperNetEx.Recipes](https://www.nuget.org/packages/ZooKeeperNetEx.Recipes)


#### Build From Source
##### Prerequisites
1. [Oracle JDK](http://www.oracle.com/technetwork/java/javase/downloads/index.html).
1. [Apache Ant](http://ant.apache.org/manual/install.html).
2. [Visual Studio](https://visualstudio.microsoft.com/vs/).

##### Build Steps
1. Run `ant` on the repository's root folder.
3. Open `src\csharp\ZooKeeperNetEx.sln` with Visual Studio.
4. Build.
