

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Thu Sep 10 17:04:59 2009
 */
/* Compiler settings for _MouseTrackerLib.idl:
    Oicf, W1, Zp8, env=Win32 (32b run)
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
//@@MIDL_FILE_HEADING(  )

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef ___MouseTrackerLib_h__
#define ___MouseTrackerLib_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ITracker_FWD_DEFINED__
#define __ITracker_FWD_DEFINED__
typedef interface ITracker ITracker;
#endif 	/* __ITracker_FWD_DEFINED__ */


#ifndef __CTracker_FWD_DEFINED__
#define __CTracker_FWD_DEFINED__

#ifdef __cplusplus
typedef class CTracker CTracker;
#else
typedef struct CTracker CTracker;
#endif /* __cplusplus */

#endif 	/* __CTracker_FWD_DEFINED__ */


/* header files for imported files */
#include "prsht.h"
#include "mshtml.h"
#include "mshtmhst.h"
#include "exdisp.h"
#include "objsafe.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ITracker_INTERFACE_DEFINED__
#define __ITracker_INTERFACE_DEFINED__

/* interface ITracker */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_ITracker;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("62881EF1-7089-40EE-A5F7-31FBD2F7BD70")
    ITracker : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE StartTracker( 
            /* [in] */ VARIANT_BOOL ClearPreviousTrack) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE StopTracker( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_IsTracking( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetTrack_Raw( 
            /* [in] */ LONGLONG hWnd,
            /* [retval][out] */ BSTR *TrackXML) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetTrack_No_Redundent( 
            /* [in] */ LONGLONG hWnd,
            /* [retval][out] */ BSTR *TrackXML) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetTrack_No_Collinear( 
            /* [in] */ LONGLONG hWnd,
            /* [in] */ DOUBLE CollinearThreshold,
            /* [retval][out] */ BSTR *TrackXML) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ITrackerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ITracker * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ITracker * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ITracker * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ITracker * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ITracker * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ITracker * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ITracker * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *StartTracker )( 
            ITracker * This,
            /* [in] */ VARIANT_BOOL ClearPreviousTrack);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *StopTracker )( 
            ITracker * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_IsTracking )( 
            ITracker * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetTrack_Raw )( 
            ITracker * This,
            /* [in] */ LONGLONG hWnd,
            /* [retval][out] */ BSTR *TrackXML);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetTrack_No_Redundent )( 
            ITracker * This,
            /* [in] */ LONGLONG hWnd,
            /* [retval][out] */ BSTR *TrackXML);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetTrack_No_Collinear )( 
            ITracker * This,
            /* [in] */ LONGLONG hWnd,
            /* [in] */ DOUBLE CollinearThreshold,
            /* [retval][out] */ BSTR *TrackXML);
        
        END_INTERFACE
    } ITrackerVtbl;

    interface ITracker
    {
        CONST_VTBL struct ITrackerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ITracker_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ITracker_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ITracker_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ITracker_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ITracker_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ITracker_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ITracker_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ITracker_StartTracker(This,ClearPreviousTrack)	\
    ( (This)->lpVtbl -> StartTracker(This,ClearPreviousTrack) ) 

#define ITracker_StopTracker(This)	\
    ( (This)->lpVtbl -> StopTracker(This) ) 

#define ITracker_get_IsTracking(This,pVal)	\
    ( (This)->lpVtbl -> get_IsTracking(This,pVal) ) 

#define ITracker_GetTrack_Raw(This,hWnd,TrackXML)	\
    ( (This)->lpVtbl -> GetTrack_Raw(This,hWnd,TrackXML) ) 

#define ITracker_GetTrack_No_Redundent(This,hWnd,TrackXML)	\
    ( (This)->lpVtbl -> GetTrack_No_Redundent(This,hWnd,TrackXML) ) 

#define ITracker_GetTrack_No_Collinear(This,hWnd,CollinearThreshold,TrackXML)	\
    ( (This)->lpVtbl -> GetTrack_No_Collinear(This,hWnd,CollinearThreshold,TrackXML) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ITracker_INTERFACE_DEFINED__ */



#ifndef __MouseTrackerLib_LIBRARY_DEFINED__
#define __MouseTrackerLib_LIBRARY_DEFINED__

/* library MouseTrackerLib */
/* [helpstring][uuid][version] */ 


EXTERN_C const IID LIBID_MouseTrackerLib;

EXTERN_C const CLSID CLSID_CTracker;

#ifdef __cplusplus

class DECLSPEC_UUID("098069F8-F34C-44D6-93DA-7D64BD744381")
CTracker;
#endif
#endif /* __MouseTrackerLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


