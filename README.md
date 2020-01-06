# STATUS: Under Development

This project is under active development and not ready for use, yet.

-----

# Breezy - Multi-platform cloud storage file system

![Azure DevOps builds (branch)](https://img.shields.io/azure-devops/build/griffinplus/0c5429e2-3d59-4fb6-9b83-f9d93ea1d794/19/master?label=Build)
![Azure DevOps tests (branch)](https://img.shields.io/azure-devops/tests/griffinplus/breezy/19/master?label=Tests)

## Overview

*Breezy* is a local file system backed by cloud storage to combine the speed of local disks and the capacity of cloud
storage. It is available for *Windows* and *Linux*. On Windows it presents itself to the user as a regular drive while
it integrates into Linux as a FUSE based file system. It improves privacy by encrypting data on the fly. Thus *Breezy*
ensures your data can only be read by you making public clouds a private place.
