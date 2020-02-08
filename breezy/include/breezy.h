﻿#ifndef BREEZY_H_
#define BREEZY_H_

////////////////////////////////////////////////////////////////////////////////
// C++ includes
////////////////////////////////////////////////////////////////////////////////
#include "BreezyCore.h"

////////////////////////////////////////////////////////////////////////////////
// API function result type
////////////////////////////////////////////////////////////////////////////////
typedef int BREEZY_API_RESULT;

////////////////////////////////////////////////////////////////////////////////
// error codes
////////////////////////////////////////////////////////////////////////////////
#define BREEZY_ERROR_SUCCESS (0)
#define BREEZY_ERROR_NOT_INITIALIZED (1)

////////////////////////////////////////////////////////////////////////////////
// functions
////////////////////////////////////////////////////////////////////////////////
#ifdef __cplusplus
extern "C" {
#endif

BREEZY_API_RESULT init_breezy();
BREEZY_API_RESULT shutdown_breezy();

#ifdef __cplusplus
}
#endif

#endif // BREEZY_H_