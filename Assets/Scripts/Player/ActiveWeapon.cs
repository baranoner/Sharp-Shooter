using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TMP_Text ammoText;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Animator animator;
    WeaponSO currentWeaponSO;
    Weapon currentWeapon;
    float timeSinceLastShot = 0f;
    float lensDefaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        lensDefaultFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }
    void Start(){
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }
   
    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if(currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString("D2");
        
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
       if(currentWeapon)
       {
        Destroy(currentWeapon.gameObject);
       }
       Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
       currentWeapon = newWeapon;
       this.currentWeaponSO = weaponSO;
       AdjustAmmo(weaponSO.MagazineSize);       
    }
    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;
        if(timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0;
            AdjustAmmo(-1);

        }
        if(!currentWeaponSO.IsAutomatic)
        {
        starterAssetsInputs.ShootInput(false);
        }
       
        
    }
    void HandleZoom()
    {
        if(!currentWeaponSO.CanZoom) return;
        

        if(starterAssetsInputs.zoom)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
           firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = lensDefaultFOV;
            weaponCamera.fieldOfView = lensDefaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);

            
        }
    }
}
