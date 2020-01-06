cmake_minimum_required( VERSION 3.12 )

# set name of the library (without extension)
set( lib dokan1 )
set( DOKANY_ROOT "${CMAKE_CURRENT_SOURCE_DIR}/project" )

include_directories ( "${DOKANY_ROOT}/sys" )

# add library target
add_library( ${lib} SHARED )

# add source files to target
target_sources( ${lib}
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

# set compiler-specific flags
target_compile_definitions( ${lib} PRIVATE UNICODE UNICODE )
target_compile_definitions( ${lib} PRIVATE _EXPORTING )
