# 项目介绍

![image](https://github.com/maifeipin/WeGpt/assets/166213212/6bba9778-e853-40f9-9b95-a6df3c971b03)

![image](https://github.com/maifeipin/WeGpt/assets/166213212/f981e9e5-80c1-43e8-bd00-7dbe96f981c9)


## 项目名称: WeGpt

WeGpt 是一个基于 WinForm 的项目，旨在通过集成 WebView2 来展示各种生成式 AI 产品。它使用数据库管理 AI 产品的项目地址和对话记录，并计划后续集成视频下载和文档合并功能。其主要特色在于利用 [NaiveProxy](https://github.com/klzgrad/naiveproxy) 和 [Brook](https://github.com/txthinking/brook) 客户端执行命令行与 WebView 的 Proxy-Server 参数配置，以便在不同的网络环境中轻松切换，从而减少国家和区域的隔离问题。这些参数存储在本地 SQLite 数据库中。通过应用程序执行这些命令和参数，可实现不同网络之间的无缝切换，包括隐藏在 CDN 后面的真实 IP 地址，有效保护免受网络攻击行为的影响。

## 项目依赖

- .NET Framework 4.5.2
- WebView2
- SQLite.Interop
- System.Data.SQLite

## 开源协议

该项目采用 Apache License 2.0  协议。 

## 插件服务端的配置
- 参考原项目文档
- 整理笔记[简单安装过程](https://maifeipin.com/archives/yong-naiveproxy-da-zao-zi-ji-de-zhuan-shu-liu-lan-qi--er-)
