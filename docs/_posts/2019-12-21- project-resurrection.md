---
layout: post
title:  "Project Resurrection"
date:   2019-12-21 13:20:00
author: Sascha Falk
---

After a period of inactivity of more than a year, Project *Breezy* continues. The original plans could not be
implemented as we had hoped. However, we are still convinced of the vision of making public clouds a private
place and are therefore starting over again with fresh ideas.

The originally planned all-in-one solution does not exist. Instead, there will be separate solutions adapted
to the platforms (Windows, Linux and MacOS) that use a common component for the core logic. So *Breezy* will be
a drive on Windows and a FUSE file system on Linux. For MacOS there are no concrete ideas yet, because no one
of us owns a Mac.

The originally planned proxy mode could be realized by an S3 server connected to drive/filesystem provided by
*Breezy*. A realization with [minio](https://min.io/) could become an option.

Ideas and the interest to contribute are very welcome.

Well then, here we go again!
