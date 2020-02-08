cmake_minimum_required( VERSION 3.12 )

# set name of the library (without extension)
set( lib breezy )

# add windows specific source files to target
if( WIN32 AND BUILD_SHARED_LIBS )
    set( CMAKE_MODULE_PATH "${CMAKE_MODULE_PATH}" "${CMAKE_CURRENT_SOURCE_DIR}/../CMakeHelpers" )
    include( generate_product_version )
    generate_product_version(
        ProductVersionFiles
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
        ICON "${CMAKE_CURRENT_SOURCE_DIR}/product.ico"
    )
    set( sources ${sources} PRIVATE ${ProductVersionFiles} )
    set( sources ${sources} PRIVATE src/dllmain.cpp )
endif()

# add project specific source files
set( sources ${sources}
     PRIVATE src/breezy.def
     PRIVATE src/BreezyCore.cpp
     PRIVATE src/init.cpp )

# prepare settings common to dynamically and statically linked libraries
set( common_target_compile_definitions PRIVATE _UNICODE UNICODE )

# add target for dynamically linked library
if( BUILD_SHARED_LIBS )
    add_library( breezy SHARED )
    set_target_properties( breezy PROPERTIES OUTPUT_NAME breezy )
    target_sources( breezy ${sources} )
    target_compile_definitions( breezy ${common_target_compile_definitions} )

    # Name output .so files "breezy.1.0.0.0" which is pretty useful
    set_target_properties( breezy PROPERTIES
        VERSION ${PROJECT_VERSION}
        SOVERSION ${PROJECT_VERSION}
    )
endif()

# add target for statically linked library
if( BUILD_STATIC_LIBS )
    add_library( breezy_static STATIC )
    set_target_properties( breezy_static PROPERTIES OUTPUT_NAME breezy )
    target_sources( breezy_static ${sources} )
    target_compile_definitions( breezy_static ${common_target_compile_definitions} )
endif()

# define the include directories used when building
include_directories( "${CMAKE_CURRENT_SOURCE_DIR}/include" )

# -----------------------------------------------------------------------------
# install googletest for unit-testing
# (integration as described at https://github.com/google/googletest/)
# -----------------------------------------------------------------------------

# Download and unpack googletest at configure time
configure_file( CMakeLists.txt.in googletest-download/CMakeLists.txt )
execute_process(
    COMMAND ${CMAKE_COMMAND} -G "${CMAKE_GENERATOR}" .
    RESULT_VARIABLE result
    WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/googletest-download )
if( result )
    message( FATAL_ERROR "CMake step for googletest failed: ${result}" )
endif()
execute_process(
    COMMAND ${CMAKE_COMMAND} --build .
    RESULT_VARIABLE result
    WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR}/googletest-download )
if( result )
    message( FATAL_ERROR "Build step for googletest failed: ${result}" )
endif()

# Prevent overriding the parent project's compiler/linker settings on Windows
# set( gtest_force_shared_crt ON CACHE BOOL "" FORCE )

# -----------------------------------------------------------------------------
# add unit test target
# -----------------------------------------------------------------------------

set( sources
     PRIVATE test/init.cpp )

if( BUILD_STATIC_LIBS )

    # Add googletest directly to the build (defines the gtest and gtest_main targets)
    set( LAST_BUILD_SHARED_LIBS ${BUILD_SHARED_LIBS} )
    set( BUILD_SHARED_LIBS OFF )  # trick to build googletest as a static library
    add_subdirectory(
        ${CMAKE_CURRENT_BINARY_DIR}/googletest-src
        ${CMAKE_CURRENT_BINARY_DIR}/googletest-build
        EXCLUDE_FROM_ALL
    )
    set( BUILD_SHARED_LIBS ${LAST_BUILD_SHARED_LIBS} )

    add_executable( breezy_test )
    target_link_libraries( breezy_test gtest_main )
    target_link_libraries( breezy_test breezy_static )
    target_sources( breezy_test ${sources} )

elseif( BUILD_SHARED_LIBS )

    # Add googletest directly to the build (defines the gtest and gtest_main targets)
    add_subdirectory(
        ${CMAKE_CURRENT_BINARY_DIR}/googletest-src
        ${CMAKE_CURRENT_BINARY_DIR}/googletest-build
        EXCLUDE_FROM_ALL
    )

    add_executable( breezy_test )
    target_link_libraries( breezy_test gtest_main )
    target_link_libraries( breezy_test breezy )
    target_sources( breezy_test ${sources} )
endif()
