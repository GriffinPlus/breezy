cmake_minimum_required (VERSION 3.12)

# set name of the executable (without extension)
set(exe breezyfs)

# add executable target
add_executable(${exe})

# add source files to target
target_sources(${exe}
	PRIVATE src/main.cpp
)

# add include directories
include_directories(
	"${CMAKE_CURRENT_SOURCE_DIR}/include"
	"${CMAKE_CURRENT_SOURCE_DIR}/../breezy/include"
)

# link with the breezy core library
target_link_libraries(${exe} breezy)

if (WIN32)
	# generate a resource file on Windows
	set (CMAKE_MODULE_PATH "${CMAKE_MODULE_PATH}" "${CMAKE_CURRENT_SOURCE_DIR}/../CMakeHelpers")
	include(generate_product_version)
	generate_product_version(ProductVersionFiles
		NAME "BreezyFS"
		BUNDLE "Breezy"
		FILE_DESCRIPTION "The Breezy file system for Windows"
		ORIGINAL_FILENAME "${exe}${CMAKE_EXECUTABLE_SUFFIX}"
		VERSION_MAJOR "${PROJECT_VERSION_MAJOR}"
		VERSION_MINOR "${PROJECT_VERSION_MINOR}"
		VERSION_PATCH "${PROJECT_VERSION_PATCH}"
		VERSION_REVISION "${PROJECT_VERSION_TWEAK}"
		VERSION_COMMENT "${PROJECT_INFORMATIONAL_VERSION}"
		COMPANY_NAME "Griffin+"
		COMPANY_COPYRIGHT "Copyright (c) 2019 Griffin+"
		ICON "${CMAKE_CURRENT_SOURCE_DIR}/product.ico")
	target_sources(${exe} PRIVATE ${ProductVersionFiles})

	# add include directories
	include_directories(
		"${CMAKE_CURRENT_SOURCE_DIR}/../3rdParty/dokany/project/dokan"
		"${CMAKE_CURRENT_SOURCE_DIR}/../3rdParty/dokany/project/sys")

	# add libraries
	target_link_libraries(${exe} dokan1)
endif()

# Set compiler-specific flags
target_compile_definitions(${exe} PRIVATE UNICODE UNICODE)
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")

	# GCC

elseif ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "MSVC")

	# Visual Studio

endif()
