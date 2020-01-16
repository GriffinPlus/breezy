cmake_minimum_required( VERSION 3.12 )

# set name of the executable (without extension)
set( exe breezyfs )

# add executable target
add_executable( ${exe} )

if (WIN32)
    # generate a resource file on Windows
    set( CMAKE_MODULE_PATH "${CMAKE_MODULE_PATH}" "${CMAKE_CURRENT_SOURCE_DIR}/../CMakeHelpers" )
    include( generate_product_version )
    generate_product_version(
        ProductVersionFiles
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
        COMPANY_COPYRIGHT "Copyright (c) 2020 Griffin+"
        ICON "${CMAKE_CURRENT_SOURCE_DIR}/product.ico"
    )
    set( sources ${sources} PRIVATE ${ProductVersionFiles} )

    # add include directories
    include_directories(
        "${CMAKE_CURRENT_SOURCE_DIR}/../3rdParty/dokany/project/dokan"
        "${CMAKE_CURRENT_SOURCE_DIR}/../3rdParty/dokany/project/sys"
    )

    # add libraries
    target_link_libraries( ${exe} dokan1 )
endif()

# link with the breezy core library
target_link_libraries( ${exe} breezy )

# add project specific source files
set( sources ${sources} 
     PRIVATE src/main.cpp
)

# associate sources with the library targets
target_sources( ${exe} ${sources} )

# add include directories
include_directories(
    "${CMAKE_CURRENT_SOURCE_DIR}/include"
    "${CMAKE_CURRENT_SOURCE_DIR}/../breezy/include"
)

# Set compiler-specific flags
set( compile_definitions PRIVATE UNICODE UNICODE )
target_compile_definitions( ${exe} ${compile_definitions} )

if( "${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU" )

	# GCC

elseif ( "${CMAKE_CXX_COMPILER_ID}" STREQUAL "MSVC" )

	# Visual Studio

endif()
