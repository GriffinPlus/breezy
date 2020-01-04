#include "breezy.h"
#include <atomic>
#include <mutex>

static BreezyCore* g_pBreezyCore = nullptr;
static int g_nRefCount;
static std::mutex g_initMutex;

BREEZY_API_RESULT init_breezy()
{
	std::lock_guard<std::mutex> lock(g_initMutex);
	if (g_nRefCount++ == 1) {
		g_pBreezyCore = new BreezyCore();
	}
	return BREEZY_ERROR_SUCCESS;
}

BREEZY_API_RESULT shutdown_breezy()
{
	std::lock_guard<std::mutex> lock(g_initMutex);
	if (g_nRefCount == 0) {
		return BREEZY_ERROR_NOT_INITIALIZED;
	}
	delete g_pBreezyCore;
	g_pBreezyCore = nullptr;
	g_nRefCount--;
	return BREEZY_ERROR_SUCCESS;
}
