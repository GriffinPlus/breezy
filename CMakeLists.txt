cmake_minimum_required (VERSION 3.12)

set (PROJECT_VERSION "0.0.0.0")
project(breezy VERSION ${PROJECT_VERSION})

set (CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set (CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/lib)
set (CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/bin)

if (WIN32)
	set (CMAKE_IMPORT_LIBRARY_SUFFIX ".dll.lib")
	set (CMAKE_STATIC_LIBRARY_SUFFIX ".lib")
	add_subdirectory(3rdParty/dokany)
endif()

add_subdirectory(breezy)
add_subdirectory(breezyfs)

if (WIN32)
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
