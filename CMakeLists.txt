cmake_minimum_required( VERSION 3.12 )

option( BUILD_SHARED_LIBS       "Build shared library"          ON)
option( BUILD_STATIC_LIBS       "Build static library"          ON)
option( BUILD_STATIC_APPS       "Build statically linked apps"  ON)
option( LINK_RUNTIME_STATICALLY "Link runtime statically"       ON)

if( NOT CMAKE_BUILD_TYPE )
    message( STATUS "No build type selected, default to Release" )
    set( CMAKE_BUILD_TYPE "Release" )
endif()

if( NOT DEFINED PROJECT_VERSION )
    set( PROJECT_VERSION "0.0.0.0" )
endif()

project( breezy VERSION ${PROJECT_VERSION} )

# print default compiler flags (good point to start when overwriting them in the next step)
message( "CMAKE_CXX_COMPILER_ID is ${CMAKE_CXX_COMPILER_ID}" )
message( "CMAKE_C_FLAGS_DEBUG is ${CMAKE_C_FLAGS_DEBUG}" )
message( "CMAKE_C_FLAGS_RELEASE is ${CMAKE_C_FLAGS_RELEASE}" )
message( "CMAKE_C_FLAGS_RELWITHDEBINFO is ${CMAKE_C_FLAGS_RELWITHDEBINFO}" )
message( "CMAKE_C_FLAGS_MINSIZEREL is ${CMAKE_C_FLAGS_MINSIZEREL}" )
message( "CMAKE_CXX_FLAGS_DEBUG is ${CMAKE_CXX_FLAGS_DEBUG}" )
message( "CMAKE_CXX_FLAGS_RELEASE is ${CMAKE_CXX_FLAGS_RELEASE}" )
message( "CMAKE_CXX_FLAGS_RELWITHDEBINFO is ${CMAKE_CXX_FLAGS_RELWITHDEBINFO}" )
message( "CMAKE_CXX_FLAGS_MINSIZEREL is ${CMAKE_CXX_FLAGS_MINSIZEREL}" )

if( "${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU" )

    if( LINK_RUNTIME_STATICALLY )
        set( runtime_flags "-static-libstdc++ -static-libgcc" )
    else()
        set( runtime_flags "-shared-libgcc" )
    endif()

    set( CMAKE_C_FLAGS_DEBUG            "${runtime_flags} -g" )
    set( CMAKE_C_FLAGS_RELEASE          "${runtime_flags} -O3 -DNDEBUG" )
    set( CMAKE_C_FLAGS_RELWITHDEBINFO   "${runtime_flags} -O2 -g -DNDEBUG" )
    set( CMAKE_C_FLAGS_MINSIZEREL       "${runtime_flags} -Os -DNDEBUG" )
    set( CMAKE_CXX_FLAGS_DEBUG          "${runtime_flags} -g" )
    set( CMAKE_CXX_FLAGS_RELEASE        "${runtime_flags} -O3 -DNDEBUG" )
    set( CMAKE_CXX_FLAGS_RELWITHDEBINFO "${runtime_flags} -O2 -g -DNDEBUG" )
    set( CMAKE_CXX_FLAGS_MINSIZEREL     "${runtime_flags} -Os -DNDEBUG" )

elseif( "${CMAKE_CXX_COMPILER_ID}" STREQUAL "MSVC" )

    if( LINK_RUNTIME_STATICALLY )
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

if (WIN32)
    # remove preceding 'lib' and adjust file extensions
    set( CMAKE_SHARED_LIBRARY_PREFIX "" )
    set( CMAKE_SHARED_LIBRARY_SUFFIX ".dll" )
    set( CMAKE_IMPORT_LIBRARY_PREFIX "" )
    set( CMAKE_IMPORT_LIBRARY_SUFFIX ".lib" )
    set( CMAKE_STATIC_LIBRARY_PREFIX "" )
    set( CMAKE_STATIC_LIBRARY_SUFFIX "_static.lib" )

    add_subdirectory( 3rdParty/dokany )
endif()

add_subdirectory( breezy )
add_subdirectory( breezyfs )

# install toolchain specific runtime files
install( FILES ${TOOLCHAIN_RUNTIME_FILES}
         TYPE BIN )

# install targets
set( install_targets breezyfs breezy breezy_test )
if( BUILD_SHARED_LIBS )
    set( install_targets ${install_targets} breezy gtest gtest_main )
    if( WIN32 )
        set( install_targets ${install_targets} dokan1 )
    endif()
endif()
install(
    TARGETS ${install_targets}
    RUNTIME DESTINATION bin
    LIBRARY DESTINATION lib
    ARCHIVE DESTINATION lib)

