

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0500 */
/* at Fri Nov 13 17:07:41 2009
 */
/* Compiler settings for _Recognition.idl:
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

#ifndef ___Recognition_h__
#define ___Recognition_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IPrimitiveRecognition_FWD_DEFINED__
#define __IPrimitiveRecognition_FWD_DEFINED__
typedef interface IPrimitiveRecognition IPrimitiveRecognition;
#endif 	/* __IPrimitiveRecognition_FWD_DEFINED__ */


#ifndef __CPrimitiveRecognition_FWD_DEFINED__
#define __CPrimitiveRecognition_FWD_DEFINED__

#ifdef __cplusplus
typedef class CPrimitiveRecognition CPrimitiveRecognition;
#else
typedef struct CPrimitiveRecognition CPrimitiveRecognition;
#endif /* __cplusplus */

#endif 	/* __CPrimitiveRecognition_FWD_DEFINED__ */


/* header files for imported files */
#include "prsht.h"
#include "mshtml.h"
#include "mshtmhst.h"
#include "exdisp.h"
#include "objsafe.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IPrimitiveRecognition_INTERFACE_DEFINED__
#define __IPrimitiveRecognition_INTERFACE_DEFINED__

/* interface IPrimitiveRecognition */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IPrimitiveRecognition;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("88869CCD-F59E-4985-B4D8-EAE83C0D0F67")
    IPrimitiveRecognition : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadTemplates( 
            /* [in] */ BSTR TemplatePath) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE RecognizeObjects( 
            /* [in] */ BSTR strokeXML,
            /* [retval][out] */ BSTR *resultXML) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IPrimitiveRecognitionVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IPrimitiveRecognition * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IPrimitiveRecognition * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IPrimitiveRecognition * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IPrimitiveRecognition * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IPrimitiveRecognition * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IPrimitiveRecognition * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IPrimitiveRecognition * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadTemplates )( 
            IPrimitiveRecognition * This,
            /* [in] */ BSTR TemplatePath);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *RecognizeObjects )( 
            IPrimitiveRecognition * This,
            /* [in] */ BSTR strokeXML,
            /* [retval][out] */ BSTR *resultXML);
        
        END_INTERFACE
    } IPrimitiveRecognitionVtbl;

    interface IPrimitiveRecognition
    {
        CONST_VTBL struct IPrimitiveRecognitionVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IPrimitiveRecognition_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IPrimitiveRecognition_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IPrimitiveRecognition_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IPrimitiveRecognition_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IPrimitiveRecognition_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IPrimitiveRecognition_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IPrimitiveRecognition_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IPrimitiveRecognition_LoadTemplates(This,TemplatePath)	\
    ( (This)->lpVtbl -> LoadTemplates(This,TemplatePath) ) 

#define IPrimitiveRecognition_RecognizeObjects(This,strokeXML,resultXML)	\
    ( (This)->lpVtbl -> RecognizeObjects(This,strokeXML,resultXML) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IPrimitiveRecognition_INTERFACE_DEFINED__ */



#ifndef __Recognition_LIBRARY_DEFINED__
#define __Recognition_LIBRARY_DEFINED__

/* library Recognition */
/* [helpstring][uuid][version] */ 


EXTERN_C const IID LIBID_Recognition;

EXTERN_C const CLSID CLSID_CPrimitiveRecognition;

#ifdef __cplusplus

class DECLSPEC_UUID("D8E8E148-F05C-4862-B6BC-A55C28E97F86")
CPrimitiveRecognition;
#endif
#endif /* __Recognition_LIBRARY_DEFINED__ */

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


