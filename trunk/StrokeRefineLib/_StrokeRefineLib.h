

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Wed Apr 14 01:30:44 2010
 */
/* Compiler settings for _StrokeRefineLib.idl:
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

#ifndef ___StrokeRefineLib_h__
#define ___StrokeRefineLib_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IStrokeRefinement_FWD_DEFINED__
#define __IStrokeRefinement_FWD_DEFINED__
typedef interface IStrokeRefinement IStrokeRefinement;
#endif 	/* __IStrokeRefinement_FWD_DEFINED__ */


#ifndef __CStrokeRefinement_FWD_DEFINED__
#define __CStrokeRefinement_FWD_DEFINED__

#ifdef __cplusplus
typedef class CStrokeRefinement CStrokeRefinement;
#else
typedef struct CStrokeRefinement CStrokeRefinement;
#endif /* __cplusplus */

#endif 	/* __CStrokeRefinement_FWD_DEFINED__ */


/* header files for imported files */
#include "prsht.h"
#include "mshtml.h"
#include "mshtmhst.h"
#include "exdisp.h"
#include "objsafe.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IStrokeRefinement_INTERFACE_DEFINED__
#define __IStrokeRefinement_INTERFACE_DEFINED__

/* interface IStrokeRefinement */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IStrokeRefinement;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FDAE69D6-93E3-4815-9FB7-78457D9B7FED")
    IStrokeRefinement : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Refine_MergeVertex( 
            /* [in] */ BSTR StrokeXML,
            /* [in] */ DOUBLE DistanceThreshold,
            /* [retval][out] */ BSTR *ResultXML) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IStrokeRefinementVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IStrokeRefinement * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IStrokeRefinement * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IStrokeRefinement * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IStrokeRefinement * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IStrokeRefinement * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IStrokeRefinement * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IStrokeRefinement * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Refine_MergeVertex )( 
            IStrokeRefinement * This,
            /* [in] */ BSTR StrokeXML,
            /* [in] */ DOUBLE DistanceThreshold,
            /* [retval][out] */ BSTR *ResultXML);
        
        END_INTERFACE
    } IStrokeRefinementVtbl;

    interface IStrokeRefinement
    {
        CONST_VTBL struct IStrokeRefinementVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IStrokeRefinement_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IStrokeRefinement_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IStrokeRefinement_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IStrokeRefinement_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IStrokeRefinement_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IStrokeRefinement_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IStrokeRefinement_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IStrokeRefinement_Refine_MergeVertex(This,StrokeXML,DistanceThreshold,ResultXML)	\
    ( (This)->lpVtbl -> Refine_MergeVertex(This,StrokeXML,DistanceThreshold,ResultXML) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IStrokeRefinement_INTERFACE_DEFINED__ */



#ifndef __StrokeRefineLib_LIBRARY_DEFINED__
#define __StrokeRefineLib_LIBRARY_DEFINED__

/* library StrokeRefineLib */
/* [helpstring][uuid][version] */ 


EXTERN_C const IID LIBID_StrokeRefineLib;

EXTERN_C const CLSID CLSID_CStrokeRefinement;

#ifdef __cplusplus

class DECLSPEC_UUID("1A512878-0A46-4EB9-9BED-7F902AF26A5F")
CStrokeRefinement;
#endif
#endif /* __StrokeRefineLib_LIBRARY_DEFINED__ */

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


