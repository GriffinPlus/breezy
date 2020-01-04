#include <windows.h>
#include <breezy.h>

BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	switch (fdwReason)
	{
		case DLL_PROCESS_ATTACH:
			init_breezy();
			break;
		case DLL_PROCESS_DETACH:
			shutdown_breezy();
			break;
	}

	return TRUE;
}