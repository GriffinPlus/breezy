cmake_minimum_required( VERSION 3.12 )

if( NOT DEFINED PROJECT_VERSION )
    set( PROJECT_VERSION "0.0.0.0" )
endif()

project( breezy VERSION ${PROJECT_VERSION} )

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

if( WIN32 )
    install(
        TARGETS breezyfs breezy breezy_test dokan1
        RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib
        ARCHIVE DESTINATION lib)
else()
    install(
        TARGETS breezyfs breezy breezy_test
        RUNTIME DESTINATION bin
        LIBRARY DESTINATION lib
        ARCHIVE DESTINATION lib)
endif()

