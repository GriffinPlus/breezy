cmake_minimum_required( VERSION 3.12 )

option( BUILD_SHARED_LIBS       "Build shared libraries"              ON  )
option( BUILD_STATIC_LIBS       "Build static libraries"              ON  )
option( BUILD_STATIC_APPS       "Build statically linked apps"        ON  )
option( INSTALL_TOOLCHAIN_FILES "Install toolchain runtime libraries" ON  )

if( NOT CMAKE_BUILD_TYPE )
    message( STATUS "No build type selected, default to Release" )
    set( CMAKE_BUILD_TYPE "Release" )
endif()

if( NOT DEFINED PROJECT_VERSION )
    set( PROJECT_VERSION "0.0.0.0" )
endif()

project( breezy VERSION ${PROJECT_VERSION} )

# print default compiler flags (good point to start when overwriting them in the next step)
# message( "CMAKE_CXX_COMPILER_ID is ${CMAKE_CXX_COMPILER_ID}" )
# message( "CMAKE_C_FLAGS_DEBUG is ${CMAKE_C_FLAGS_DEBUG}" )
# message( "CMAKE_C_FLAGS_RELEASE is ${CMAKE_C_FLAGS_RELEASE}" )
# message( "CMAKE_C_FLAGS_RELWITHDEBINFO is ${CMAKE_C_FLAGS_RELWITHDEBINFO}" )
# message( "CMAKE_C_FLAGS_MINSIZEREL is ${CMAKE_C_FLAGS_MINSIZEREL}" )
# message( "CMAKE_CXX_FLAGS_DEBUG is ${CMAKE_CXX_FLAGS_DEBUG}" )
# message( "CMAKE_CXX_FLAGS_RELEASE is ${CMAKE_CXX_FLAGS_RELEASE}" )
# message( "CMAKE_CXX_FLAGS_RELWITHDEBINFO is ${CMAKE_CXX_FLAGS_RELWITHDEBINFO}" )
# message( "CMAKE_CXX_FLAGS_MINSIZEREL is ${CMAKE_CXX_FLAGS_MINSIZEREL}" )

# set RPATH to create relocalizable executables
if( NOT BUILD_STATIC_APPS )
    set( CMAKE_INSTALL_RPATH "$ORIGIN/../lib:$ORIGIN/" )
endif()

if( "${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU" )

    if( BUILD_STATIC_APPS )
        set( CMAKE_EXE_LINKER_FLAGS    "-static" )
    endif()

    set( CMAKE_C_FLAGS_DEBUG            "${runtime_flag} ${CMAKE_C_FLAGS_DEBUG}" )
    set( CMAKE_C_FLAGS_RELEASE          "${runtime_flag} ${CMAKE_C_FLAGS_RELEASE}" )
    set( CMAKE_C_FLAGS_RELWITHDEBINFO   "${runtime_flag} ${CMAKE_C_FLAGS_RELWITHDEBINFO}" )
    set( CMAKE_C_FLAGS_MINSIZEREL       "${runtime_flag} ${CMAKE_C_FLAGS_MINSIZEREL}" )
    set( CMAKE_CXX_FLAGS_DEBUG          "${runtime_flag} ${CMAKE_CXX_FLAGS_DEBUG}" )
    set( CMAKE_CXX_FLAGS_RELEASE        "${runtime_flag} ${CMAKE_CXX_FLAGS_RELEASE}" )
    set( CMAKE_CXX_FLAGS_RELWITHDEBINFO "${runtime_flag} ${CMAKE_CXX_FLAGS_RELWITHDEBINFO}" )
    set( CMAKE_CXX_FLAGS_MINSIZEREL     "${runtime_flag} ${CMAKE_CXX_FLAGS_MINSIZEREL}" )

elseif( "${CMAKE_CXX_COMPILER_ID}" STREQUAL "MSVC" )

    if( BUILD_STATIC_APPS )
        set( runtime_flag "/MT" )
    else()
        set( runtime_flag "/MD" )
    endif()

    set( CMAKE_C_FLAGS_DEBUG            "${runtime_flag}d /EHsc /Zi /Ob0 /Od /RTC1" )
    set( CMAKE_C_FLAGS_RELEASE          "${runtime_flag}  /EHsc /O2 /Ob2 /DNDEBUG" )
    set( CMAKE_C_FLAGS_RELWITHDEBINFO   "${runtime_flag}  /EHsc /Zi /O2 /Ob1 /DNDEBUG" )
    set( CMAKE_C_FLAGS_MINSIZEREL       "${runtime_flag}  /EHsc /O1 /Ob1 /DNDEBUG" )
    set( CMAKE_CXX_FLAGS_DEBUG          "${runtime_flag}d /EHsc /Zi /Ob0 /Od /RTC1" )
    set( CMAKE_CXX_FLAGS_RELEASE        "${runtime_flag}  /EHsc /O2 /Ob2 /DNDEBUG" )
    set( CMAKE_CXX_FLAGS_RELWITHDEBINFO "${runtime_flag}  /EHsc /Zi /O2 /Ob1 /DNDEBUG" )
    set( CMAKE_CXX_FLAGS_MINSIZEREL     "${runtime_flag}  /EHsc /O1 /Ob1 /DNDEBUG" )

endif()

set( CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib )
set( CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib )
set( CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin )

if( WIN32 )
    # remove preceding 'lib' and adjust file extensions
    set( CMAKE_SHARED_LIBRARY_PREFIX "" )
    set( CMAKE_SHARED_LIBRARY_SUFFIX ".dll" )
    set( CMAKE_IMPORT_LIBRARY_PREFIX "" )
    set( CMAKE_IMPORT_LIBRARY_SUFFIX ".lib" )
    set( CMAKE_STATIC_LIBRARY_PREFIX "" )
    set( CMAKE_STATIC_LIBRARY_SUFFIX "_static.lib" )
endif()

# include targets to build shared libraries
add_subdirectory( breezy )
if( WIN32 )
    add_subdirectory( 3rdParty/dokany )
endif()

# include targets to build executables
add_subdirectory( breezyfs )

# install targets
set( install_targets breezyfs breezy_test )
if( BUILD_SHARED_LIBS )
    set( install_targets ${install_targets} breezy_shared gtest gtest_main )
    if( WIN32 )
        set( install_targets ${install_targets} dokan1_shared )
    endif()
endif()
install(
    TARGETS ${install_targets}
    RUNTIME DESTINATION bin
    LIBRARY DESTINATION lib
    ARCHIVE DESTINATION lib)
