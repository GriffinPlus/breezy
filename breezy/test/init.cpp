#include <breezy.h>
#include "gtest/gtest.h"

using namespace std;

TEST(Init, init_and_shutdown_breezy)
{
	BREEZY_API_RESULT result;
	result = init_breezy();
	EXPECT_EQ(result, BREEZY_ERROR_SUCCESS);
	result = shutdown_breezy();
	EXPECT_EQ(result, BREEZY_ERROR_SUCCESS);
}
