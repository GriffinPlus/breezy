cmake_minimum_required( VERSION 3.12 )

set( DOKANY_ROOT "${CMAKE_CURRENT_SOURCE_DIR}/project" )

# add source files to target
set( sources
    PRIVATE "${DOKANY_ROOT}/dokan/access.c"
    PRIVATE "${DOKANY_ROOT}/dokan/cleanup.c"
    PRIVATE "${DOKANY_ROOT}/dokan/close.c"
    PRIVATE "${DOKANY_ROOT}/dokan/create.c"
    PRIVATE "${DOKANY_ROOT}/dokan/directory.c"
    PRIVATE "${DOKANY_ROOT}/dokan/dokan.c"
    PRIVATE "${DOKANY_ROOT}/dokan/dokan.def"
    PRIVATE "${DOKANY_ROOT}/dokan/dokan.rc"
    PRIVATE "${DOKANY_ROOT}/dokan/fileinfo.c"
    PRIVATE "${DOKANY_ROOT}/dokan/flush.c"
    PRIVATE "${DOKANY_ROOT}/dokan/lock.c"
    PRIVATE "${DOKANY_ROOT}/dokan/mount.c"
    PRIVATE "${DOKANY_ROOT}/dokan/ntstatus.c"
    PRIVATE "${DOKANY_ROOT}/dokan/read.c"
    PRIVATE "${DOKANY_ROOT}/dokan/security.c"
    PRIVATE "${DOKANY_ROOT}/dokan/setfile.c"
    PRIVATE "${DOKANY_ROOT}/dokan/timeout.c"
    PRIVATE "${DOKANY_ROOT}/dokan/version.c"
    PRIVATE "${DOKANY_ROOT}/dokan/volume.c"
    PRIVATE "${DOKANY_ROOT}/dokan/write.c"
)

# prepare settings common to dynamically and statically linked libraries
set( common_target_compile_definitions PRIVATE _UNICODE UNICODE _EXPORTING )

# add target for shared library
if( BUILD_SHARED_LIBS )
    add_library( dokan1_shared SHARED )
    set_target_properties( dokan1_shared PROPERTIES OUTPUT_NAME dokan1 )
    target_sources( dokan1_shared ${sources} )
    target_compile_definitions( dokan1_shared ${common_target_compile_definitions} )
endif()

# add target for static library
if( BUILD_STATIC_LIBS )
    add_library( dokan1_static STATIC )
    set_target_properties( dokan1_static PROPERTIES OUTPUT_NAME dokan1 )
    target_sources( dokan1_static ${sources} )
    target_compile_definitions( dokan1_static ${common_target_compile_definitions} )
endif()

# set include directories
include_directories( AFTER "${DOKANY_ROOT}/sys" )
