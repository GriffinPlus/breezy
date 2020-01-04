cmake_minimum_required (VERSION 3.12)

# set name of the library (without extension)
set (lib breezy)

# add library target
add_library(${lib} SHARED)

# generate a resource file on Windows
if (WIN32)
	set (CMAKE_MODULE_PATH "${CMAKE_MODULE_PATH}" "${CMAKE_CURRENT_SOURCE_DIR}/../CMakeHelpers")
	include(generate_product_version)
	generate_product_version(ProductVersionFiles
		NAME "Breezy Core"
		BUNDLE "Breezy"
		FILE_DESCRIPTION "Core of the Breezy file system"
		ORIGINAL_FILENAME "${CMAKE_SHARED_LIBRARY_PREFIX}${lib}${CMAKE_SHARED_LIBRARY_SUFFIX}"
		VERSION_MAJOR "${PROJECT_VERSION_MAJOR}"
		VERSION_MINOR "${PROJECT_VERSION_MINOR}"
		VERSION_PATCH "${PROJECT_VERSION_PATCH}"
		VERSION_REVISION "${PROJECT_VERSION_TWEAK}"
		VERSION_COMMENT "${PROJECT_INFORMATIONAL_VERSION}"
		COMPANY_NAME "Griffin+"
		COMPANY_COPYRIGHT "Copyright (c) 2019 Griffin+"
		ICON "${CMAKE_CURRENT_SOURCE_DIR}/product.ico")
	target_sources(${lib} PRIVATE ${ProductVersionFiles})
endif()

# add source files to target
target_sources(${lib}
	PRIVATE src/BreezyCore.cpp
	PRIVATE src/init.cpp
)

# add windows specific source files to target
if (WIN32)
	target_sources(${lib} PRIVATE src/dllmain.cpp)
endif()

include_directories(
	"${CMAKE_CURRENT_SOURCE_DIR}/include"
)

# Name output .so files "mylib.1.0" which is pretty useful
set_target_properties(${lib} PROPERTIES
	VERSION ${PROJECT_VERSION}
	SOVERSION ${PROJECT_VERSION}
)

# Set compiler-specific flags
target_compile_definitions(${lib} PRIVATE UNICODE UNICODE)
target_compile_definitions(${lib} PRIVATE BREEZY_EXPORT)

if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")

	# GCC

elseif ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "MSVC")

	# MSVC

endif()

# -----------------------------------------------------------------------------
# install googletest for unit-testing
# (integration as described at https://github.com/google/googletest/)
# -----------------------------------------------------------------------------

# Download and unpack googletest at configure time
configure_file(CMakeLists.txt.in googletest-download/CMakeLists.txt)
execute_process(COMMAND ${CMAKE_COMMAND} -G "${CMAKE_GENERATOR}" .
	RESULT_VARIABLE result
	WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/googletest-download )
if(result)
	message(FATAL_ERROR "CMake step for googletest failed: ${result}")
endif()
execute_process(COMMAND ${CMAKE_COMMAND} --build .
	RESULT_VARIABLE result
	WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/googletest-download )
if(result)
	message(FATAL_ERROR "Build step for googletest failed: ${result}")
endif()

# Prevent overriding the parent project's compiler/linker settings on Windows
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)

# Add googletest directly to our build.
# This defines the gtest and gtest_main targets.
add_subdirectory(
	${CMAKE_CURRENT_BINARY_DIR}/googletest-src
	${CMAKE_CURRENT_BINARY_DIR}/googletest-build
	EXCLUDE_FROM_ALL)

# The gtest/gtest_main targets carry header search path
# dependencies automatically when using CMake 2.8.11 or
# later. Otherwise we have to add them here ourselves.
if (CMAKE_VERSION VERSION_LESS 2.8.11)
	include_directories("${gtest_SOURCE_DIR}/include")
endif()

# -----------------------------------------------------------------------------
# add unit test target
# -----------------------------------------------------------------------------

add_executable(breezy_test)
target_link_libraries(breezy_test gtest_main)
target_link_libraries(breezy_test ${lib})
target_sources(breezy_test
	PRIVATE test/init.cpp
)
